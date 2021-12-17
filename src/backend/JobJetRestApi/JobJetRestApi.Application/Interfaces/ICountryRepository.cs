using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface ICountryRepository
    {
        public Country GetById(int id);
        public bool Exists(int id);
    }
}