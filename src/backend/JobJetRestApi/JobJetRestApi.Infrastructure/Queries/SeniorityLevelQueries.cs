using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using JobJetRestApi.Infrastructure.Factories;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class SeniorityLevelQueries : ISeniorityLevelQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMemoryCache _memoryCache;
        
        public SeniorityLevelQueries(ISqlConnectionFactory sqlConnectionFactory, 
            IMemoryCache memoryCache)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
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
            
            var cacheKey = "seniorityLevelsKey";
            
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<SeniorityLevelResponse> seniorityLevels))
            {
                seniorityLevels = await connection.QueryAsync<SeniorityLevelResponse>(query, new
                {
                    OffsetRows = paginationFilter.PageNumber,
                    FetchRows = paginationFilter.PageSize
                });
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
            
                _memoryCache.Set(cacheKey, seniorityLevels, cacheExpiryOptions);
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