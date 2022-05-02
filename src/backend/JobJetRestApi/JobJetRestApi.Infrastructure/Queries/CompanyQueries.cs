using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.Companies.Queries;
using Dapper;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class CompanyQueries : ICompanyQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly record struct CompanyRecord(int Id, string Name, string ShortName, string Description, int NumberOfPeople, string CityName);
        
        public CompanyQueries(ISqlConnectionFactory sqlConnectionFactory)
        {
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

            var queriedCompanies = await connection.QueryAsync<CompanyRecord>(query, new
            {
                OffsetRows = paginationFilter.PageNumber,
                FetchRows = paginationFilter.GetNormalizedPageSize()
            });

            var companies = queriedCompanies.Select(x => 
                new CompanyResponse(x.Id, x.Name, x.ShortName, x.Description, x.NumberOfPeople, x.Name));

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
            
            var queriedCompany = await connection.QueryFirstOrDefaultAsync<CompanyRecord>(query, new
            {
                Id = id
            });
            
            if (queriedCompany.Id == 0)
            {
                throw CompanyNotFoundException.ForId(id);
            }

            var company = new CompanyResponse(
                queriedCompany.Id,
                queriedCompany.Name,
                queriedCompany.ShortName,
                queriedCompany.Description,
                queriedCompany.NumberOfPeople,
                queriedCompany.CityName);

            return company;
        }
    }
}