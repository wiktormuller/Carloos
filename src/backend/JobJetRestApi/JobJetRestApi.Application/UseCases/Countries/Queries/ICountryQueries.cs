using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.Countries.Queries
{
    public interface ICountryQueries
    {
        Task<IEnumerable<CountryResponse>> GetAllCountriesAsync(PaginationFilter paginationFilter);
    }
}