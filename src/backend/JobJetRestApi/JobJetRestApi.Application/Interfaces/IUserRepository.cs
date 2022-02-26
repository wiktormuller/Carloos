using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Exists(string email);
        Task<bool> Exists(int id);
        Task<int> Create(User user, string password);
        Task Update(User user);
        Task<User> GetById(int id);
        Task<List<User>> GetAll();
        Task Delete(int id);
    }
}