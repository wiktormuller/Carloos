using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string email);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsWithUserName(string userName);
        Task<int> CreateAsync(User user, string password);
        Task UpdateAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<List<string>> GetUserRolesAsync(User user);
        Task AssignRoleToUser(User user, Role role);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}