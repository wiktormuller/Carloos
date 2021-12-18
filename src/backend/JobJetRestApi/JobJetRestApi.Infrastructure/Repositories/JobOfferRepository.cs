using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class JobOfferRepository : IJobOfferRepository
    {
        private readonly JobJetDbContext _jobJetDbContext;

        public JobOfferRepository(JobJetDbContext jobJetDbContext)
        {
            _jobJetDbContext = jobJetDbContext;
        }

        public JobOffer GetById(int id)
        {
            return _jobJetDbContext.JobOffers.Find(id);
        }

        public bool Exists(int id)
        {
            return GetById(id) is not null;
        }

        public int Create(JobOffer jobOffer)
        {
            _jobJetDbContext.JobOffers.Add(jobOffer);
            var jobOfferId = _jobJetDbContext.SaveChanges();

            return jobOffer.Id;
        }

        public void Update()
        {
            _jobJetDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var jobJetDbContext = GetById(id);
            _jobJetDbContext.Remove(jobJetDbContext);

            _jobJetDbContext.SaveChanges();
        }
    }
}