using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobJetRestApi.Web.Installers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
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
        }
    }
}