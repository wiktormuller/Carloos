using System;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using JobJetRestApi.Infrastructure.Repositories;
using JobJetRestApi.Infrastructure.Services;
using JobJetRestApi.Web.Installers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Companies.Queries;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using JobJetRestApi.Application.Validators.RequestsValidators;
using JobJetRestApi.Infrastructure.Dtos;
using JobJetRestApi.Infrastructure.Factories;
using JobJetRestApi.Infrastructure.Queries;

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
            // @TODO - Split services into separate files
            
            services.InstallServicesInAssembly(Configuration);
            
            // IdentityModelEventSource.ShowPII = true;
            services.AddDbContext<JobJetDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<ISeniorityRepository, SeniorityRepository>();
            services.AddScoped<ITechnologyTypeRepository, TechnologyTypeRepository>();
            services.AddScoped<IEmploymentTypeRepository, EmploymentTypeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<IGeocodingService, GeocodingService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ICacheService, CacheService>();

            services.AddScoped<ICompanyQueries, CompanyQueries>();
            services.AddScoped<ICountryQueries, CountryQueries>();
            services.AddScoped<ICurrencyQueries, CurrencyQueries>();
            services.AddScoped<IEmploymentTypeQueries, EmploymentTypeQueries>();
            services.AddScoped<IJobOfferQueries, JobOfferQueries>();
            services.AddScoped<ISeniorityLevelQueries, SeniorityLevelQueries>();
            services.AddScoped<ITechnologyTypeQueries, TechnologyTypeQueries>();
            
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            
            
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateUserRequestValidator>());

            services.Configure<GeocodingOptions>(Configuration.GetSection(GeocodingOptions.Geocoding));
            services.Configure<GeoRouteOptions>(Configuration.GetSection(GeoRouteOptions.GeoRoute));
            services.Configure<DatabaseOptions>(Configuration.GetSection(DatabaseOptions.ConnectionStrings));
            
            services.AddMediatR(AppDomain.CurrentDomain.Load("JobJetRestApi.Application"));
            
            services.AddMemoryCache();
            
            services.AddHealthChecksUI()
                .AddInMemoryStorage();
            
            services.AddHttpContextAccessor();
            services.AddSingleton<IPageUriService>(o => // Here we get the base URL of the application http(s)://www.jobjet.com
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = request.Scheme + "://" + request.Host.ToUriComponent();
                return new PageUriService(uri);
            });
            
            // For Identity
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<JobJetDbContext>()
                .AddDefaultTokenProviders();
            
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
            
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            
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
                //app.UseDeveloperExceptionPage();
            }
            app.UseDeveloperExceptionPage();
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobJetRestApi.Web v1"));

            //app.UseHttpsRedirection(); // Docker cannot serve the app with this middleware, why?

            app.UseRouting();
            
            //app.UseCors("default"); // Refers to services.AddCors
            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health-checks", new HealthCheckOptions()
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                
                endpoints.MapHealthChecksUI(config => config.UIPath = "/health-checks-ui");
            });
        }
    }
}