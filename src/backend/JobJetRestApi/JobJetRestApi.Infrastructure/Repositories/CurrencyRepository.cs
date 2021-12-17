using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public CurrencyRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public Currency GetById(int id)
        {
            return _jobJetDbContext.Currencies.Find(id);
        }

        public bool Exists(int id)
        {
            return GetById(id) is not null;
        }
    }
}