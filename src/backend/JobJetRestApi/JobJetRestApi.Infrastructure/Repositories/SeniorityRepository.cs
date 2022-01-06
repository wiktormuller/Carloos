using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class SeniorityRepository : ISeniorityRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public SeniorityRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public async Task<Seniority> GetById(int id)
        {
            return await _jobJetDbContext.SeniorityLevels.FindAsync(id);
        }

        public async Task<List<Seniority>> GetAll()
        {
            return  await _jobJetDbContext.SeniorityLevels.ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await GetById(id) is not null;
        }

        public async Task<bool> Exists(string name)
        {
            var seniorityLevel = await _jobJetDbContext.SeniorityLevels
                .FirstOrDefaultAsync(x => x.Name == name);

            return seniorityLevel is not null;
        }

        public async Task<int> Create(Seniority seniority)
        {
            await _jobJetDbContext.AddAsync(seniority);
            await _jobJetDbContext.SaveChangesAsync();

            return seniority.Id;
        }

        public async Task Update()
        {
            await _jobJetDbContext.SaveChangesAsync();
        }

        public async Task Delete(Seniority seniority)
        {
            _jobJetDbContext.Remove(seniority);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}