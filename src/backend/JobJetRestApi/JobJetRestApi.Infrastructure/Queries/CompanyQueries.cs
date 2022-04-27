using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.Companies.Queries;
using Dapper;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Infrastructure.Configuration;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class CompanyQueries : ICompanyQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;
        
        public CompanyQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
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

            var companies = _cacheService.Get<IEnumerable<CompanyResponse>>(CacheKeys.CompaniesListKey);
                
            if (companies is null)
            {
                companies = await connection.QueryAsync<CompanyResponse>(query, new
                {
                    OffsetRows = paginationFilter.PageNumber,
                    FetchRows = paginationFilter.PageSize
                });

                _cacheService.Add(companies, CacheKeys.CompaniesListKey);
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