using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Common.Config;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class SeniorityLevelQueries : ISeniorityLevelQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;
        
        public SeniorityLevelQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
        }
    
        public async Task<IEnumerable<SeniorityLevelResponse>> GetAllSeniorityLevelsAsync(PaginationFilter paginationFilter)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [SeniorityLevel].Id,
                    [SeniorityLevel].Name
                FROM [SeniorityLevels] AS [SeniorityLevel] 
                ORDER BY [SeniorityLevel].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
                ;
            
            var seniorityLevels = _cacheService.Get<IEnumerable<SeniorityLevelResponse>>(CacheKeys.SeniorityTypesListKey);
                
            if (seniorityLevels is null)
            {
                seniorityLevels = await connection.QueryAsync<SeniorityLevelResponse>(query, new
                {
                    OffsetRows = paginationFilter.PageNumber,
                    FetchRows = paginationFilter.PageSize
                });

                _cacheService.Add(seniorityLevels, CacheKeys.SeniorityTypesListKey);
            }

            return seniorityLevels;
        }

        public async Task<SeniorityLevelResponse> GetSeniorityLevelByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [SeniorityLevel].Id,
                    [SeniorityLevel].Name
                FROM [SeniorityLevels] AS [SeniorityLevel] 
                WHERE [SeniorityLevel.Id = @Id
                ORDER BY [SeniorityLevel].Id;"
                ;
            
            var seniorityLevel = await connection.QueryFirstOrDefaultAsync<SeniorityLevelResponse>(query, new
            {
                Id = id
            });

            if (seniorityLevel is null)
            {
                throw SeniorityLevelNotFoundException.ForId(id);
            }

            return seniorityLevel;
        }
    }
}