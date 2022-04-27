using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using JobJetRestApi.Infrastructure.Configuration;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class CountryQueries : ICountryQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;
        
        public CountryQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
        }
        
        public async Task<IEnumerable<CountryResponse>> GetAllCountriesAsync(PaginationFilter paginationFilter)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [Country].Id,
                    [Country].Name,
                    [Country].Alpha2Code,
                    [Country].Alpha3Code,
                    [Country].NumericCode 
                FROM [Countries] AS [Country] 
                ORDER BY [Country].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
                ;
            
            var countries = _cacheService.Get<IEnumerable<CountryResponse>>(CacheKeys.CountriesListKey);
                
            if (countries is null)
            {
                countries = await connection.QueryAsync<CountryResponse>(query, new
                {
                    OffsetRows = paginationFilter.PageNumber,
                    FetchRows = paginationFilter.PageSize
                });

                _cacheService.Add(countries, CacheKeys.CountriesListKey);
            }

            return countries;
        }

        /// <exception cref="CountryNotFoundException"></exception>
        public async Task<CountryResponse> GetCountryByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [Country].Id,
                    [Country].Name,
                    [Country].Alpha2Code,
                    [Country].Alpha3Code,
                    [Country].NumericCode 
                FROM [Countries] AS [Country] 
                WHERE [Country].Id = @Id
                ORDER BY [Country].Id;"
                ;
            
            var country = await connection.QueryFirstOrDefaultAsync<CountryResponse>(query, new
            {
                Id = id
            });

            if (country is null)
            {
                throw CountryNotFoundException.ForId(id);
            }

            return country;
        }
    }
}