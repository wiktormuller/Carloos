using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface ICurrencyRepository
    {
        Currency GetById(int id);
        bool Exists(int id);
    }
}