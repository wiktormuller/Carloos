using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Repositories
{
    public interface ITechnologyTypeRepository
    {
        Task<TechnologyType> GetByIdAsync(int id);
        Task<List<TechnologyType>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string name);
        Task<int> CreateAsync(TechnologyType technologyType);
        Task UpdateAsync();
        Task DeleteAsync(TechnologyType technologyType);
    }
}