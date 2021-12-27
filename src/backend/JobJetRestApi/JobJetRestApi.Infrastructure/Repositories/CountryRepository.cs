using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public CountryRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public async Task<Country> GetById(int id)
        {
            return await _jobJetDbContext.Countries.FindAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await GetById(id) is not null;
        }

        public async Task<bool> Exists(string name)
        {
            var country = await _jobJetDbContext.Countries
                .FirstOrDefaultAsync(x => x.Name == name);

            return country is not null;
        }

        public async Task<bool> Exists(string name, string alpha2Code, string alpha3Code, int numericCode)
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

        public async Task<int> Create(Country country)
        {
            await _jobJetDbContext.Countries.AddAsync(country);
            await _jobJetDbContext.SaveChangesAsync();

            return country.Id;
        }

        public async Task Update()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task Delete(Country country)
        {
            _jobJetDbContext.Countries.Remove(country);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}