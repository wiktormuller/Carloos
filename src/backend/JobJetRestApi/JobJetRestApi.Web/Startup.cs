using System;
using FluentValidation.AspNetCore;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using JobJetRestApi.Infrastructure.Repositories;
using JobJetRestApi.Infrastructure.Services;
using JobJetRestApi.Infrastructure.Validators;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace JobJetRestApi.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // IdentityModelEventSource.ShowPII = true;
            services.AddDbContext<JobJetDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(AppDomain.CurrentDomain.Load("JobJetRestApi.Application"));

            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<ISeniorityRepository, SeniorityRepository>();
            services.AddScoped<ITechnologyTypeRepository, TechnologyTypeRepository>();
            services.AddScoped<IEmploymentTypeRepository, EmploymentTypeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IGeocodingService, GeocodingService>();
            services.AddScoped<IRouteService, RouteService>();
            
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateJobOfferRequestValidator>());

            services.AddHttpClient<IGeocodingService, GeocodingService>();
            services.AddHttpClient<IRouteService, RouteService>();

            services.Configure<GeocodingOptions>(Configuration.GetSection(GeocodingOptions.Geocoding));
            services.Configure<GeoRouteOptions>(Configuration.GetSection(GeoRouteOptions.GeoRoute));
            
            services.AddHttpContextAccessor();
            services.AddSingleton<IPageUriService>(o => // Here we get the base URL of the application http(s)://www.jobjet.com
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = request.Scheme + "://" + request.Host.ToUriComponent();
                return new PageUriService(uri);
            });
            
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.ApiName = "jobjetapi";
                    options.Authority = "https://localhost:5001"; // How to force https?
                    options.RequireHttpsMetadata = false; // For dev purposes
                });
            
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:5005")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "JobJetRestApi.Web", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobJetRestApi.Web v1"));

            //app.UseHttpsRedirection(); // Docker cannot serve the app with this middleware, why?

            app.UseRouting();
            
            app.UseCors("default");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}