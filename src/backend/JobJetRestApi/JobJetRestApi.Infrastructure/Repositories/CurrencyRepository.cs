using System.Collections.Generic;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public CurrencyRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = Guard.Against.Null(jobJetDbContext, nameof(jobJetDbContext));
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            return await _jobJetDbContext.Currencies.FindAsync(id);
        }

        public async Task<List<Currency>> GetAllAsync()
        {
            return await _jobJetDbContext.Currencies.ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await GetByIdAsync(id) is not null;
        }

        public async Task<bool> ExistsAsync(string isoCode, int isoNumber)
        {
            var currency = await _jobJetDbContext.Currencies
                .FirstOrDefaultAsync(x => x.IsoCode == isoCode || x.IsoNumber == isoNumber);

            return currency is not null;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var currency = await _jobJetDbContext.Currencies
                .FirstOrDefaultAsync(x => x.Name == name);

            return currency is not null;
        }

        public async Task<int> CreateAsync(Currency currency)
        {
            await _jobJetDbContext.AddAsync(currency);
            await _jobJetDbContext.SaveChangesAsync();

            return currency.Id;
        }

        public async Task UpdateAsync()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Currency currency)
        {
            _jobJetDbContext.Currencies.Remove(currency);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}