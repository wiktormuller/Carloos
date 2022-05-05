using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.UseCases.Users.Queries;

public interface IUserQueries
{
    Task<(IEnumerable<UserResponse> Users, int TotalCount)> GetAllUsersAsync(PaginationFilter paginationFilter);
    Task<UserResponse> GetUserByIdAsync(int id);
}