using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using JobJetRestApi.Infrastructure.Configuration;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class CurrencyQueries : ICurrencyQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;
        
        public CurrencyQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
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
            
            var currencies = _cacheService.Get<IEnumerable<CurrencyResponse>>(CacheKeys.CurrenciesListKey);
                
            if (currencies is null)
            {
                currencies = await connection.QueryAsync<CurrencyResponse>(query, new
                {
                    OffsetRows = paginationFilter.PageNumber,
                    FetchRows = paginationFilter.PageSize
                });

                _cacheService.Add(currencies, CacheKeys.CurrenciesListKey);
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