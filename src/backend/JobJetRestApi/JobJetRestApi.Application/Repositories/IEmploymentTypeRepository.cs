using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Repositories
{
    public interface IEmploymentTypeRepository
    {
        Task<EmploymentType> GetByIdAsync(int id);
        Task<List<EmploymentType>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string name);
        Task<int> CreateAsync(EmploymentType employmentType);
        Task DeleteAsync(EmploymentType employmentType);
    }
}