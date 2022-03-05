﻿using JobJetRestApi.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobJetRestApi.Web.Installers
{
    public class OptionInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GeocodingOptions>(configuration.GetSection(GeocodingOptions.Geocoding));
            services.Configure<GeoRouteOptions>(configuration.GetSection(GeoRouteOptions.GeoRoute));
            services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.ConnectionStrings));
        }
    }
}