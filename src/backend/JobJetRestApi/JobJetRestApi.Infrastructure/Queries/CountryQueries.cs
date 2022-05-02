using System.Collections.Generic;
using System.Linq;
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

        private readonly record struct CountryRecord(int Id, string Name, string Alpha2Code, string Alpha3Code, int NumericCode);
        
        public CountryQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
        }
        
        public async Task<IEnumerable<CountryResponse>> GetAllCountriesAsync()
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
                ORDER BY [Country].Id"
                ;
            
            var countries = _cacheService.Get<IEnumerable<CountryResponse>>(CacheKeys.CountriesListKey);
                
            if (countries is null)
            {
                var queriedCountries = await connection.QueryAsync<CountryRecord>(query);

                countries = queriedCountries.Select(x => new CountryResponse(x.Id, x.Name, x.Alpha2Code));
                
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
            
            var queriedCountry = await connection.QueryFirstOrDefaultAsync<CountryRecord>(query, new
            {
                Id = id
            });
            
            if (queriedCountry.Id == 0)
            {
                throw CountryNotFoundException.ForId(id);
            }

            var country = new CountryResponse(
                queriedCountry.Id, 
                queriedCountry.Name, 
                queriedCountry.Alpha2Code);

            return country;
        }
    }
}