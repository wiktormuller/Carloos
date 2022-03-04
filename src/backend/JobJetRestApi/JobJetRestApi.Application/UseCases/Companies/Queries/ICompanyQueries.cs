using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.Companies.Queries
{
    public interface ICompanyQueries
    {
        Task<IEnumerable<CompanyResponse>> GetAllCompaniesAsync(PaginationFilter paginationFilter);
        Task<CompanyResponse> GetCompanyByIdAsync(int id);
    }
}