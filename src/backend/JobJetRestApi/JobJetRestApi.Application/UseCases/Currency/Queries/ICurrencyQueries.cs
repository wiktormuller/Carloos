using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.Currency.Queries
{
    public interface ICurrencyQueries
    {
        Task<IEnumerable<CurrencyResponse>> GetAllCurrenciesAsync();
        Task<CurrencyResponse> GetCurrencyByIdAsync(int id);
    }
}