using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;
        
        public CompanyRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = Guard.Against.Null(jobJetDbContext, nameof(jobJetDbContext));
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _jobJetDbContext.Companies.FindAsync(id);
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _jobJetDbContext.Companies.ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await GetByIdAsync(id) is not null;
        }
        
        public async Task<bool> ExistsAsync(string name)
        {
            var company = await _jobJetDbContext.Companies
                .FirstOrDefaultAsync(x => x.Name == name);

            return company is not null;
        }

        public async Task<int> CreateAsync(Company company)
        {
            await _jobJetDbContext.Companies.AddAsync(company);
            await _jobJetDbContext.SaveChangesAsync();

            return company.Id;
        }

        public async Task UpdateAsync()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Company company)
        {
            _jobJetDbContext.Companies.Remove(company);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}