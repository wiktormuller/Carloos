using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface ITechnologyTypeRepository
    {
        Task<TechnologyType> GetById(int id);
        Task<List<TechnologyType>> GetAll();
        Task<bool> Exists(int id);
        Task<bool> Exists(string name);
        Task<int> Create(TechnologyType technologyType);
        Task Update();
        Task Delete(TechnologyType technologyType);
    }
}