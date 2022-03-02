using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class TechnologyTypeRepository : ITechnologyTypeRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public TechnologyTypeRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = Guard.Against.Null(jobJetDbContext, nameof(jobJetDbContext));
        }

        public async Task<TechnologyType> GetByIdAsync(int id)
        {
            return await _jobJetDbContext.TechnologyTypes.FindAsync(id);
        }

        public async Task<List<TechnologyType>> GetAllAsync()
        {
            return await _jobJetDbContext.TechnologyTypes.ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await GetByIdAsync(id) is not null;
        }
        public async Task<bool> ExistsAsync(string name)
        {
            var technologyType = await _jobJetDbContext.TechnologyTypes
                .FirstOrDefaultAsync(x => x.Name == name);

            return technologyType is not null;
        }

        public async Task<int> CreateAsync(TechnologyType technologyType)
        {
            await _jobJetDbContext.TechnologyTypes.AddAsync(technologyType);
            await _jobJetDbContext.SaveChangesAsync();

            return technologyType.Id;
        }

        public async Task UpdateAsync()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TechnologyType technologyType)
        {
            _jobJetDbContext.Remove(technologyType);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}