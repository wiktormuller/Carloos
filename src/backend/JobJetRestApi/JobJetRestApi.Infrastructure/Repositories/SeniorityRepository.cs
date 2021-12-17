using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class SeniorityRepository : ISeniorityRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public SeniorityRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public Seniority GetById(int id)
        {
            return _jobJetDbContext.SeniorityLevels.Find(id);
        }

        public bool Exists(int id)
        {
            return GetById(id) is not null;
        }
    }
}