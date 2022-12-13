using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class SeniorityRepository : ISeniorityRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public SeniorityRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = Guard.Against.Null(jobJetDbContext, nameof(jobJetDbContext));
        }

        public async Task<Seniority> GetByIdAsync(int id)
        {
            return await _jobJetDbContext.SeniorityLevels.FindAsync(id);
        }

        public async Task<List<Seniority>> GetAllAsync()
        {
            return  await _jobJetDbContext.SeniorityLevels.ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await GetByIdAsync(id) is not null;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var seniorityLevel = await _jobJetDbContext.SeniorityLevels
                .FirstOrDefaultAsync(x => x.Name == name);

            return seniorityLevel is not null;
        }

        public async Task<int> CreateAsync(Seniority seniority)
        {
            await _jobJetDbContext.AddAsync(seniority);
            await _jobJetDbContext.SaveChangesAsync();

            return seniority.Id;
        }

        public async Task UpdateAsync()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Seniority seniority)
        {
            _jobJetDbContext.Remove(seniority);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}