using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using JobJetRestApi.Infrastructure.Services;

namespace JobJetRestApi.Web.Installers
{
    public class HealthCheckInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()

            // Azure storage
            .AddCheck(
                "JobJetAzureStorageDB - Check",
                new AzureSqlConnectionHealthCheck(configuration.GetConnectionString("DefaultConnection")),
                HealthStatus.Unhealthy,
                new string[] {"jobjetazurestoragedb"})
            
            // Google Geocoding API
            .AddUrlGroup(
                new Uri("https://google.com"),
                "Google Geocoding API - Check",
                HealthStatus.Unhealthy,
                new string[] {"googlegeocodingapi"})
            
            // Project OSRM API
            .AddUrlGroup(
                new Uri("https://google.com"),
                "Project OSRM API - Check",
                HealthStatus.Unhealthy,
                new string[] {"projectosrmapi"})
            
            // Identity Server
            .AddUrlGroup(
                new Uri("https://google.com"),
                "Identity Server - Check",
                HealthStatus.Unhealthy,
                new string[] {"identityserver"})
            
            // React client
            .AddUrlGroup(
                new Uri("https://google.com"),
                "JobJet React Client - Check",
                HealthStatus.Unhealthy,
                new string[] {"jobjetreactclient"});
        }
    }
}