using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.EmploymentType.Queries
{
    public interface IEmploymentTypeQueries
    {
        Task<IEnumerable<EmploymentTypeResponse>> GetAllEmploymentTypesAsync(PaginationFilter paginationFilter);
    }
}