using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Users.Queries;
using JobJetRestApi.Infrastructure.Factories;
using System.Linq;
using JetBrains.Annotations;

namespace JobJetRestApi.Infrastructure.Queries;

public class UserQueries : IUserQueries
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    
    private readonly record struct UserRecord(int Id, string UserName, string Email);

    private readonly record struct RefreshTokenRecord(int Id, DateTime ExpiresAt, bool IsExpired, DateTime? RevokedAt, [CanBeNull] string ReplacedByToken, string Token);

    public UserQueries(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<(IEnumerable<UserResponse> Users, int TotalCount)> GetAllUsersAsync(PaginationFilter paginationFilter)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();

        const string query = @"
            SELECT
                [User].Id,
                [User].UserName,
                [User].Email
            FROM [AspNetUsers] AS [User]
            ORDER BY [User].Id
            OFFSET @OffsetRows ROWS
            FETCH NEXT @FetchRows ROWS ONLY;"
            ;

        var queriedUsers = await connection.QueryAsync<UserRecord>(
            sql: query,
            param: new
            {
                OffsetRows = paginationFilter.PageNumber,
                FetchRows = paginationFilter.GetNormalizedPageSize()
            });

        var totalCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM [AspNetUsers];");
            
        var users = queriedUsers.Select(x => new UserResponse(x.Id, x.UserName, x.Email));

        return (users, totalCount);
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

        var queriedUser = await connection.QueryFirstOrDefaultAsync<UserRecord>(query, new
        {
            Id = id
        });

        if (queriedUser.Id == 0)
        {
            throw UserNotFoundException.ForId(id);
        }

        var user = new UserResponse(
            queriedUser.Id, 
            queriedUser.UserName, 
            queriedUser.Email);

        return user;
    }

    public async Task<List<RefreshTokenResponse>> GetRefreshTokensAsync(int id)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();

        const string query = @"
            SELECT
                [RefreshToken].Id,
                [RefreshToken].ExpiresAt,
                [RefreshToken].IsExpired,
                [RefreshToken].RevokedAt,
                [RefreshToken].ReplacedByToken,
                [RefreshToken].Token
            FROM [RefreshTokens] AS [RefreshToken]
            WHERE [RefreshToken].UserId = @Id
            ORDER BY [RefreshToken].CreatedAt;
        ";

        var queriedRefreshTokens = await connection.QueryAsync<RefreshTokenRecord>(query, new
        {
            Id = id
        });

        var refreshTokens = queriedRefreshTokens
            .Select(x => new RefreshTokenResponse(
                x.Id,
                x.IsExpired,
                x.RevokedAt == null && !x.IsExpired,
                x.ReplacedByToken,
                x.Token))
            .ToList();

        return refreshTokens;
    }
}