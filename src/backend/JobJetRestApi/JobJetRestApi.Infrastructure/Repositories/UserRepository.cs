using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = Guard.Against.Null(userManager, nameof(userManager));
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString()) is not null;
        }

        public async Task<int> CreateAsync(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
            return user.Id;
        }

        public async Task UpdateAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);

            await _userManager.DeleteAsync(user);
        }
    }
}