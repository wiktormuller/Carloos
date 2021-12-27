using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<Currency> GetById(int id);
        Task<bool> Exists(int id);
        Task<bool> Exists(string isoCode, int isoNumber);
        Task<bool> Exists(string name);
        Task<int> Create(Currency currency);
        Task Update();
        Task Delete(Currency currency);
    }
}