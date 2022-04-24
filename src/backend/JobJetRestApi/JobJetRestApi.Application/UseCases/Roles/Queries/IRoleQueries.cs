using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.Roles.Queries;

public interface IRoleQueries
{
    Task<IEnumerable<RoleResponse>> GetAllRolesAsync(PaginationFilter paginationFilter);
    Task<RoleResponse> GetRoleByIdAsync(int id);
}