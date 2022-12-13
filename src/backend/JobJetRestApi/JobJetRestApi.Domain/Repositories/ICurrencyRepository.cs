using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Domain.Repositories
{
    public interface ICurrencyRepository
    {
        Task<Currency> GetByIdAsync(int id);
        Task<List<Currency>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string isoCode, int isoNumber);
        Task<bool> ExistsAsync(string name);
        Task<int> CreateAsync(Currency currency);
        Task UpdateAsync();
        Task DeleteAsync(Currency currency);
    }
}