using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> GetById(int id);
        Task<List<Country>> GetAll();
        Task<bool> Exists(int id);
        Task<bool> Exists(string name);
        Task<bool> Exists(string name, string alpha2Code, string alpha3Code, int numericCode);
        Task<int> Create(Country country);
        Task Update();
        Task Delete(Country country);
    }
}