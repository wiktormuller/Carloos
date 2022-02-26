using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
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
            _userManager = userManager;
        }

        public async Task<bool> Exists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<bool> Exists(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString()) is not null;
        }

        public async Task<int> Create(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
            return user.Id;
        }

        public async Task Update(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<User> GetById(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<List<User>> GetAll()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task Delete(int id)
        {
            var user = await GetById(id);

            await _userManager.DeleteAsync(user);
        }
    }
}