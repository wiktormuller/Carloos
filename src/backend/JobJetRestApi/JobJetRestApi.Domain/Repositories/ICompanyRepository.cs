using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(int id);
        Task<List<Company>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string name);
        Task<int> CreateAsync(Company company);
        Task UpdateAsync();
        Task DeleteAsync(Company company);
    }
}