using System;
using FluentValidation.AspNetCore;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.Validators.RequestsValidators;
using JobJetRestApi.Infrastructure.Factories;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using JobJetRestApi.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobJetRestApi.Web.Installers
{
    public class HelperServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton<IPageUriService>(o => // Here we get the base URL of the application http(s)://www.jobjet.com
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = request.Scheme + "://" + request.Host.ToUriComponent();
                return new PageUriService(uri);
            });
            
            services.AddScoped<IGeocodingService, GeocodingService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ICacheService, CacheService>();

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            
            services.AddMediatR(AppDomain.CurrentDomain.Load("JobJetRestApi.Application"));
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateUserRequestValidator>());
            services.AddMemoryCache();
            
            // IdentityModelEventSource.ShowPII = true;
            services.AddDbContext<JobJetDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}