using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.TechnologyType.Queries
{
    public interface ITechnologyTypeQueries
    {
        Task<IEnumerable<TechnologyTypeResponse>> GetAllTechnologyTypesAsync();
        Task<TechnologyTypeResponse> GetTechnologyTypeByIdAsync(int id);
    }
}