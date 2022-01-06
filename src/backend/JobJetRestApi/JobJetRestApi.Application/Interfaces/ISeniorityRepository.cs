using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface ISeniorityRepository
    {
        Task<Seniority> GetById(int id);
        Task<List<Seniority>> GetAll();
        Task<bool> Exists(int id);
        Task<bool> Exists(string name);
        Task<int> Create(Seniority seniority);
        Task Update();
        Task Delete(Seniority seniority);
    }
}