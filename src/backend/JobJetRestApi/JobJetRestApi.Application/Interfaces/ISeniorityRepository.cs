using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface ISeniorityRepository
    {
        public Seniority GetById(int id);
        public bool Exists(int id);
    }
}