using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class EmploymentTypeRepository : IEmploymentTypeRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public EmploymentTypeRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public async Task<EmploymentType> GetById(int id)
        {
            return await _jobJetDbContext.EmploymentTypes.FindAsync(id);
        }

        public async Task<List<EmploymentType>> GetAll()
        {
            return await _jobJetDbContext.EmploymentTypes.ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await GetById(id) is not null;
        }

        public async Task<bool> Exists(string name)
        {
            var employmentType = await _jobJetDbContext.EmploymentTypes
                .FirstOrDefaultAsync(x => x.Name == name);

            return employmentType is not null;
        }

        public async Task<int> Create(EmploymentType employmentType)
        {
            await _jobJetDbContext.AddAsync(employmentType);
            await _jobJetDbContext.SaveChangesAsync();

            return employmentType.Id;
        }

        public async Task Delete(EmploymentType employmentType)
        {
            _jobJetDbContext.EmploymentTypes.Remove(employmentType);
            await _jobJetDbContext.SaveChangesAsync();
        }
    }
}