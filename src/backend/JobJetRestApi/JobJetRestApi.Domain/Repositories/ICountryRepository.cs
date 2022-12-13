using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Domain.Repositories
{
    public interface ICountryRepository
    {
        Task<Country> GetByIdAsync(int id);
        Task<List<Country>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string name);
        Task<bool> ExistsAsync(string name, string alpha2Code, string alpha3Code, int numericCode);
        Task<int> CreateAsync(Country country);
        Task UpdateAsync();
        Task DeleteAsync(Country country);
    }
}