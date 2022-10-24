using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<Role> _roleManager;
    
    public RoleRepository(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public Task<List<Role>> GetAllAsync()
    {
        return _roleManager.Roles.ToListAsync();
    }

    public Task<Role> GetByIdAsync(int id)
    {
        return _roleManager.FindByIdAsync(id.ToString());
    }

    public Task<Role> GetByNameAsync(string name)
    {
        return _roleManager.FindByNameAsync(name);
    }

    public async Task<int> CreateAsync(Role role)
    {
        var roleId = await _roleManager.CreateAsync(role);

        return role.Id;
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _roleManager.FindByNameAsync(name) is not null;
    }
}