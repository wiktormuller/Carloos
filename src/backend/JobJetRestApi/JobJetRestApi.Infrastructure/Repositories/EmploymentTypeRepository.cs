using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class EmploymentTypeRepository : IEmploymentTypeRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public EmploymentTypeRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public EmploymentType GetById(int id)
        {
            return _jobJetDbContext.EmploymentTypes.Find(id);
        }

        public bool Exists(int id)
        {
            return GetById(id) is not null;
        }
    }
}