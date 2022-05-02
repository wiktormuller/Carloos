using System.Collections.Generic;
using System.Linq;
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

        private readonly record struct CurrencyRecord(int Id, string Name, string IsoCode, int IsoNumber);
        
        public CurrencyQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
        }
        
        public async Task<IEnumerable<CurrencyResponse>> GetAllCurrenciesAsync()
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [Currency].Id,
                    [Currency].Name,
                    [Currency].IsoCode,
                    [Currency].IsoNumber
                FROM [Currencies] AS [Currency] 
                ORDER BY [Currency].Id"
                ;
            
            var currencies = _cacheService.Get<IEnumerable<CurrencyResponse>>(CacheKeys.CurrenciesListKey);
                
            if (currencies is null)
            {
                var queriedCurrencies = await connection.QueryAsync<CurrencyRecord>(query);

                currencies = queriedCurrencies.Select(x =>
                    new CurrencyResponse(x.Id, x.Name, x.IsoCode));
                
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
            
            var queriedCurrency = await connection.QueryFirstOrDefaultAsync<CurrencyRecord>(query, new
            {
                Id = id
            });

            if (queriedCurrency.Id == 0)
            {
                throw CurrencyNotFoundException.ForId(id);
            }

            var currency = new CurrencyResponse(
                queriedCurrency.Id, 
                queriedCurrency.Name, 
                queriedCurrency.IsoCode);

            return currency;
        }
    }
}