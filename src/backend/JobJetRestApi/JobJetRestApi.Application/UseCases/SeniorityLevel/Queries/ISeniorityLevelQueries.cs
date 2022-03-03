using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.Queries
{
    public interface ISeniorityLevelQueries
    {
        Task<IEnumerable<SeniorityLevelResponse>> GetAllSeniorityLevelsAsync(PaginationFilter paginationFilter);
    }
}