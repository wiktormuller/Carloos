using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Domain.Repositories
{
    public interface ITechnologyTypeRepository
    {
        Task<TechnologyType> GetByIdAsync(int id);
        Task<List<TechnologyType>> GetByIdsAsync(IEnumerable<int> ids);
        Task<List<TechnologyType>> GetAllAsync();
        Task<List<TechnologyType>> GetAllAsync(IEnumerable<int> ids);
        Task<bool> ExistsAsync(int id);
        Task<(bool Exists, IEnumerable<int> NonExistingIds)> ExistsAsync(IEnumerable<int> ids);
        Task<bool> ExistsAsync(string name);
        Task<int> CreateAsync(TechnologyType technologyType);
        Task UpdateAsync();
        Task DeleteAsync(TechnologyType technologyType);
    }
}