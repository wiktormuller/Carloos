using System.Collections.Generic;
using System.Linq;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public CurrencyRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public async Task<Currency> GetById(int id)
        {
            return await _jobJetDbContext.Currencies.FindAsync(id);
        }

        public async Task<List<Currency>> GetAll()
        {
            return await _jobJetDbContext.Currencies.ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await GetById(id) is not null;
        }

        public async Task<bool> Exists(string isoCode, int isoNumber)
        {
            var currency = await _jobJetDbContext.Currencies
                .FirstOrDefaultAsync(x => x.IsoCode == isoCode || x.IsoNumber == isoNumber);

            return currency is not null;
        }

        public async Task<bool> Exists(string name)
        {
            var currency = await _jobJetDbContext.Currencies
                .FirstOrDefaultAsync(x => x.Name == name);

            return currency is not null;
        }

        public async Task<int> Create(Currency currency)
        {
            await _jobJetDbContext.AddAsync(currency);
            await _jobJetDbContext.SaveChangesAsync();

            return currency.Id;
        }

        public async Task Update()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task Delete(Currency currency)
        {
            _jobJetDbContext.Currencies.Remove(currency);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}