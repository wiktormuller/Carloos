using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public CountryRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public Country GetById(int id)
        {
            return _jobJetDbContext.Countries.Find(id);
        }

        public bool Exists(int id)
        {
            return GetById(id) is not null;
        }
    }
}