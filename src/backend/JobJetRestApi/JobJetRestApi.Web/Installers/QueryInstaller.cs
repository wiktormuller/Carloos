using JobJetRestApi.Application.UseCases.Companies.Queries;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using JobJetRestApi.Application.UseCases.Dashboards.Queries;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using JobJetRestApi.Application.UseCases.Profiles.Queries;
using JobJetRestApi.Application.UseCases.Roles.Queries;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using JobJetRestApi.Application.UseCases.Users.Queries;
using JobJetRestApi.Infrastructure.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobJetRestApi.Web.Installers
{
    public class QueryInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICompanyQueries, CompanyQueries>();
            services.AddScoped<ICountryQueries, CountryQueries>();
            services.AddScoped<ICurrencyQueries, CurrencyQueries>();
            services.AddScoped<IEmploymentTypeQueries, EmploymentTypeQueries>();
            services.AddScoped<IJobOfferQueries, JobOfferQueries>();
            services.AddScoped<ISeniorityLevelQueries, SeniorityLevelQueries>();
            services.AddScoped<ITechnologyTypeQueries, TechnologyTypeQueries>();
            services.AddScoped<IRoleQueries, RoleQueries>();
            services.AddScoped<IUserQueries, UserQueries>();
            services.AddScoped<IDashboardQueries, DashboardQueries>();
            services.AddScoped<IProfileQueries, ProfileQueries>();
        }
    }
}