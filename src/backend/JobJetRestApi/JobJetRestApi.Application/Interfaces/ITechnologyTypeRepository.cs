using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface ITechnologyTypeRepository
    {
        public TechnologyType GetById(int id);
        public bool Exists(int id);
    }
}