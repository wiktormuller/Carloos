using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Users.Queries;
using JobJetRestApi.Infrastructure.Configuration;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries;

public class UserQueries : IUserQueries
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ICacheService _cacheService;
    
    public UserQueries(ISqlConnectionFactory sqlConnectionFactory, 
        ICacheService cacheService)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _cacheService = cacheService;
    }
    
    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync(PaginationFilter paginationFilter)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();

        const string query = @"
            SELECT
                [User].Id,
                [User].Name,
                [User].Email
            FROM [AspNetUsers] AS [User]
            ORDER BY [User].Id
            OFFSET @OffsetRows ROWS
            FETCH NEXT @FetchRows ROWS ONLY;"
            ;

        var users = _cacheService.Get<IEnumerable<UserResponse>>(CacheKeys.UsersListKey);

        if (users is null)
        {
            users = await connection.QueryAsync<UserResponse>(query, new
            {
                OffsetRows = paginationFilter.PageNumber,
                FetchRows = paginationFilter.PageSize
            });
            
            _cacheService.Add(users, CacheKeys.UsersListKey);
        }

        return users;
    }

    public async Task<UserResponse> GetUserByIdAsync(int id)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();

        const string query = @"
            SELECT
                [User].Id,
                [User].Name,
                [User].Email
            FROM [AspNetUsers] AS [User]
            WHERE [User].Id = @Id
            ORDER BY [User].Id;
        ";

        var user = await connection.QueryFirstOrDefaultAsync<UserResponse>(query, new
        {
            Id = id
        });

        if (user is null)
        {
            throw UserNotFoundException.ForId(id);
        }

        return user;
    }
}