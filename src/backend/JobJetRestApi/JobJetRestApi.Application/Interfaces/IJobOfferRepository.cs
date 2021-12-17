using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface IJobOfferRepository
    {
        public JobOffer GetById(int id);
        public bool Exists(int id);
        public int Create(JobOffer jobOffer);
        public void Update();
        public void Delete(int id);
    }
}