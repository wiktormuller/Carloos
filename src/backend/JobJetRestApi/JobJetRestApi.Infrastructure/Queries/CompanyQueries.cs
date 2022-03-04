using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.Companies.Queries;
using Dapper;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Infrastructure.Factories;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class CompanyQueries : ICompanyQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMemoryCache _memoryCache;
        
        public CompanyQueries(ISqlConnectionFactory sqlConnectionFactory, 
            IMemoryCache memoryCache)
        {
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
        }
        
        public async Task<IEnumerable<CompanyResponse>> GetAllCompaniesAsync(PaginationFilter paginationFilter)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [Company].Id,
                    [Company].Name,
                    [Company].ShortName,
                    [Company].Description,
                    [Company].NumberOfPeople,
                    [Company].CityName 
                FROM [Companies] AS [Company] 
                ORDER BY [Company].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
                ;
            
            var cacheKey = "companiesKey";
            
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<CompanyResponse> companies))
            {
                companies = await connection.QueryAsync<CompanyResponse>(query, new
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
            
                _memoryCache.Set(cacheKey, companies, cacheExpiryOptions);
            }

            return companies;
        }

        /// <exception cref="CompanyNotFoundException"></exception>
        public async Task<CompanyResponse> GetCompanyByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [Company].Id,
                    [Company].Name,
                    [Company].ShortName,
                    [Company].Description,
                    [Company].NumberOfPeople,
                    [Company].CityName 
                FROM [Companies] AS [Company] 
                WHERE [Company].Id = @Id
                ORDER BY [Company].Id;"
                ;
            
            var company = await connection.QueryFirstOrDefaultAsync<CompanyResponse>(query, new
            {
                Id = id
            });

            if (company is null)
            {
                throw CompanyNotFoundException.ForId(id);
            }

            return company;
        }
    }
}