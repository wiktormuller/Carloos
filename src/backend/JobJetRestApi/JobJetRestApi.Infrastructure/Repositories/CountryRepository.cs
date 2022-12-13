using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public CountryRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = Guard.Against.Null(jobJetDbContext, nameof(jobJetDbContext));
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _jobJetDbContext.Countries.FindAsync(id);
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _jobJetDbContext.Countries.ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await GetByIdAsync(id) is not null;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var country = await _jobJetDbContext.Countries
                .FirstOrDefaultAsync(x => x.Name == name);

            return country is not null;
        }

        public async Task<bool> ExistsAsync(string name, string alpha2Code, string alpha3Code, int numericCode)
        {
            var country = await _jobJetDbContext.Countries
                .FirstOrDefaultAsync(
                x => x.Name == name 
                     || x.Alpha2Code == alpha2Code 
                     || x.Alpha3Code == alpha3Code 
                     || x.NumericCode == numericCode
            );

            return country is not null;
        }

        public async Task<int> CreateAsync(Country country)
        {
            await _jobJetDbContext.Countries.AddAsync(country);
            await _jobJetDbContext.SaveChangesAsync();

            return country.Id;
        }

        public async Task UpdateAsync()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Country country)
        {
            _jobJetDbContext.Countries.Remove(country);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}