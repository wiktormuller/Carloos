using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Repositories;

public interface IRoleRepository
{
    Task<List<Role>> GetAllAsync();
    Task<Role> GetByIdAsync(int id);
    Task<Role> GetByNameAsync(string name);
    Task<int> CreateAsync(Role role);
    Task<bool> ExistsAsync(string name);
}