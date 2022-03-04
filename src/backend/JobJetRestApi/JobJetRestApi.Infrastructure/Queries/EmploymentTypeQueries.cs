using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using JobJetRestApi.Infrastructure.Factories;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class EmploymentTypeQueries : IEmploymentTypeQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMemoryCache _memoryCache;
        
        public EmploymentTypeQueries(ISqlConnectionFactory sqlConnectionFactory, 
            IMemoryCache memoryCache)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<EmploymentTypeResponse>> GetAllEmploymentTypesAsync(PaginationFilter paginationFilter)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [EmploymentType].Id,
                    [EmploymentType].Name 
                FROM [EmploymentTypes] AS [EmploymentType] 
                ORDER BY [EmploymentType].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
                ;
            
            var cacheKey = "employmentTypesKey";
            
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<EmploymentTypeResponse> employmentTypes))
            {
                employmentTypes = await connection.QueryAsync<EmploymentTypeResponse>(query, new
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
            
                _memoryCache.Set(cacheKey, employmentTypes, cacheExpiryOptions);
            }

            return employmentTypes;
        }

        /// <exception cref="EmploymentTypeNotFoundException"></exception>
        public async Task<EmploymentTypeResponse> GetEmploymentTypeByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [EmploymentType].Id,
                    [EmploymentType].Name 
                FROM [EmploymentTypes] AS [EmploymentType] 
                WHERE [EmploymentType].Id = @Id
                ORDER BY [EmploymentType].Id;"
                ;
            
            var employmentType = await connection.QueryFirstOrDefaultAsync<EmploymentTypeResponse>(query, new
            {
                Id = id
            });

            if (employmentType is null)
            {
                throw EmploymentTypeNotFoundException.ForId(id);
            }

            return employmentType;
        }
    }
}