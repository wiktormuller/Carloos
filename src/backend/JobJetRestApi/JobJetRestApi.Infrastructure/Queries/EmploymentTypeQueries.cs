using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Common.Config;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class EmploymentTypeQueries : IEmploymentTypeQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;
        
        public EmploymentTypeQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
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
            
            var employmentTypes = _cacheService.Get<IEnumerable<EmploymentTypeResponse>>(CacheKeys.EmploymentTypesListKey);
                
            if (employmentTypes is null)
            {
                employmentTypes = await connection.QueryAsync<EmploymentTypeResponse>(query, new
                {
                    OffsetRows = paginationFilter.PageNumber,
                    FetchRows = paginationFilter.PageSize
                });

                _cacheService.Add(employmentTypes, CacheKeys.EmploymentTypesListKey);
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