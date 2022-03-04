using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using JobJetRestApi.Infrastructure.Factories;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class TechnologyTypeQueries : ITechnologyTypeQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMemoryCache _memoryCache;
        
        public TechnologyTypeQueries(ISqlConnectionFactory sqlConnectionFactory,
            IMemoryCache memoryCache)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
        }
    
        public async Task<IEnumerable<TechnologyTypeResponse>> GetAllTechnologyTypesAsync(PaginationFilter paginationFilter)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [TechnologyType].Id,
                    [TechnologyType].Name
                FROM [TechnologyTypes] AS [TechnologyType] 
                ORDER BY [TechnologyType].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
                ;
            
            var cacheKey = "technologyTypesKey";
            
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<TechnologyTypeResponse> technologyTypes))
            {
                technologyTypes = await connection.QueryAsync<TechnologyTypeResponse>(query, new
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
            
                _memoryCache.Set(cacheKey, technologyTypes, cacheExpiryOptions);
            }

            return technologyTypes;
        }

        public async Task<TechnologyTypeResponse> GetTechnologyTypeByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [TechnologyType].Id,
                    [TechnologyType].Name
                FROM [TechnologyTypes] AS [TechnologyType] 
                WHERE [TechnologyType].Id = @Id
                ORDER BY [TechnologyType].Id;"
                ;
            
            var technologyType = await connection.QueryFirstOrDefaultAsync<TechnologyTypeResponse>(query, new
            {
                Id = id
            });

            if (technologyType is null)
            {
                throw TechnologyTypeNotFoundException.ForId(id);
            }

            return technologyType;
        }
    }
}