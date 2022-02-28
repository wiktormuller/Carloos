using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Interfaces;
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

        public async Task<TechnologyType> GetById(int id)
        {
            return await _jobJetDbContext.TechnologyTypes.FindAsync(id);
        }

        public async Task<List<TechnologyType>> GetAll()
        {
            return await _jobJetDbContext.TechnologyTypes.ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await GetById(id) is not null;
        }
        public async Task<bool> Exists(string name)
        {
            var technologyType = await _jobJetDbContext.TechnologyTypes
                .FirstOrDefaultAsync(x => x.Name == name);

            return technologyType is not null;
        }

        public async Task<int> Create(TechnologyType technologyType)
        {
            await _jobJetDbContext.TechnologyTypes.AddAsync(technologyType);
            await _jobJetDbContext.SaveChangesAsync();

            return technologyType.Id;
        }

        public async Task Update()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task Delete(TechnologyType technologyType)
        {
            _jobJetDbContext.Remove(technologyType);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}