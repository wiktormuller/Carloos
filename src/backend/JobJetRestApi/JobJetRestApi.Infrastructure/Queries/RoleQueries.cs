using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Roles.Queries;
using JobJetRestApi.Infrastructure.Configuration;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries;

public class RoleQueries : IRoleQueries
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ICacheService _cacheService;
    
    public RoleQueries(ISqlConnectionFactory sqlConnectionFactory, 
        ICacheService cacheService)
    {
        _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
        _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
    }
    
    public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync(PaginationFilter paginationFilter)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();
        
        const string query = @"
                SELECT 
                    [AspNetRole].Id,
                    [AspNetRole].Name
                FROM [AspNetRoles] AS [AspNetRole] 
                ORDER BY [AspNetRole].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
            ;

        var roles = _cacheService.Get<IEnumerable<RoleResponse>>(CacheKeys.RolesListKey);

        if (roles is null)
        {
            roles = await connection.QueryAsync<RoleResponse>(query, new
            {
                OffsetRows = paginationFilter.PageNumber,
                FetchRows = paginationFilter.PageSize
            });
            
            _cacheService.Add(roles, CacheKeys.RolesListKey);
        }

        return roles;
    }

    public async Task<RoleResponse> GetRoleByIdAsync(int id)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();

        const string query = @"
            SELECT 
                [AspNetRole].Id,
                [AspNetRole].Name
            FROM [AspNetRoles] AS [AspNetRole] 
            WHERE [AspNetRole].Id = @Id
            ORDER BY [AspNetRole].Id;"
        ;

        var role = await connection.QueryFirstOrDefaultAsync<RoleResponse>(query, new
        {
            Id = id
        });

        if (role is null)
        {
            throw RoleNotFoundException.ForId(id);
        }

        return role;
    }
}