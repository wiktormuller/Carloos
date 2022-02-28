using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Interfaces;
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

        public async Task<Company> GetById(int id)
        {
            return await _jobJetDbContext.Companies.FindAsync(id);
        }

        public async Task<List<Company>> GetAll()
        {
            return await _jobJetDbContext.Companies.ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await GetById(id) is not null;
        }
        
        public async Task<bool> Exists(string name)
        {
            var company = await _jobJetDbContext.Companies
                .FirstOrDefaultAsync(x => x.Name == name);

            return company is not null;
        }

        public async Task<int> Create(Company company)
        {
            await _jobJetDbContext.Companies.AddAsync(company);
            await _jobJetDbContext.SaveChangesAsync();

            return company.Id;
        }

        public async Task Update()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task Delete(Company company)
        {
            _jobJetDbContext.Companies.Remove(company);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}