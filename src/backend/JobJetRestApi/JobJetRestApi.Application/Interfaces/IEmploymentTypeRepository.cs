using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface IEmploymentTypeRepository
    {
        Task<EmploymentType> GetById(int id);
        Task<List<EmploymentType>> GetAll();
        Task<bool> Exists(int id);
        Task<bool> Exists(string name);
        Task<int> Create(EmploymentType employmentType);
        Task Delete(EmploymentType employmentType);
    }
}