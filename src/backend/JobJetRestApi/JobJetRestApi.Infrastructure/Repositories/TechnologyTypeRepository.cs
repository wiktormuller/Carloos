using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class TechnologyTypeRepository : ITechnologyTypeRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public TechnologyTypeRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public TechnologyType GetById(int id)
        {
            return _jobJetDbContext.TechnologyTypes.Find(id);
        }

        public bool Exists(int id)
        {
            return GetById(id) is not null;
        }
    }
}