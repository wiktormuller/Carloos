using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Repositories
{
    public interface ISeniorityRepository
    {
        Task<Seniority> GetByIdAsync(int id);
        Task<List<Seniority>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string name);
        Task<int> CreateAsync(Seniority seniority);
        Task UpdateAsync();
        Task DeleteAsync(Seniority seniority);
    }
}