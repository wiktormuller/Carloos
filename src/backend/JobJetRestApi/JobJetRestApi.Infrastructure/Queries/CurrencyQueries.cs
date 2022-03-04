using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using JobJetRestApi.Infrastructure.Factories;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class CurrencyQueries : ICurrencyQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMemoryCache _memoryCache;
        
        public CurrencyQueries(ISqlConnectionFactory sqlConnectionFactory, 
            IMemoryCache memoryCache)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _memoryCache = memoryCache;
        }
        
        public async Task<IEnumerable<CurrencyResponse>> GetAllCurrenciesAsync(PaginationFilter paginationFilter)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [Currency].Id,
                    [Currency].Name,
                    [Currency].IsoCode,
                    [Currency].IsoNumber
                FROM [Currencies] AS [Currency] 
                ORDER BY [Currency].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
                ;
            
            var cacheKey = "currenciesKey";
            
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<CurrencyResponse> currencies))
            {
                currencies = await connection.QueryAsync<CurrencyResponse>(query, new
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
            
                _memoryCache.Set(cacheKey, currencies, cacheExpiryOptions);
            }

            return currencies;
        }

        /// <exception cref="CurrencyNotFoundException"></exception>
        public async Task<CurrencyResponse> GetCurrencyByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [Currency].Id,
                    [Currency].Name,
                    [Currency].IsoCode,
                    [Currency].IsoNumber
                FROM [Currencies] AS [Currency] 
                WHERE [Currency].Id = @Id
                ORDER BY [Currency].Id;"
                ;
            
            var currency = await connection.QueryFirstOrDefaultAsync<CurrencyResponse>(query, new
            {
                Id = id
            });

            if (currency is null)
            {
                throw CurrencyNotFoundException.ForId(id);
            }

            return currency;
        }
    }
}