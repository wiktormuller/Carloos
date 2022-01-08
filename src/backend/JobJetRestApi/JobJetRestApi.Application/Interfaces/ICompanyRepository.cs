using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetById(int id);
        Task<List<Company>> GetAll();
        Task<bool> Exists(int id);
        Task<bool> Exists(string name);
        Task<int> Create(Company company);
        Task Update();
        Task Delete(Company company);
    }
}