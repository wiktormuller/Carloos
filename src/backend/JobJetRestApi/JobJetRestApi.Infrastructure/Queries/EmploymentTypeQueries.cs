using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using JobJetRestApi.Infrastructure.Configuration;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class EmploymentTypeQueries : IEmploymentTypeQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;

        private readonly record struct EmploymentTypeRecord(int Id, string Name);
        
        public EmploymentTypeQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
        }

        public async Task<IEnumerable<EmploymentTypeResponse>> GetAllEmploymentTypesAsync()
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [EmploymentType].Id,
                    [EmploymentType].Name 
                FROM [EmploymentTypes] AS [EmploymentType] 
                ORDER BY [EmploymentType].Id;"
                ;
            
            var employmentTypes = _cacheService.Get<IEnumerable<EmploymentTypeResponse>>(CacheKeys.EmploymentTypesListKey);
                
            if (employmentTypes is null)
            {
                var queriedEmploymentTypes = await connection.QueryAsync<EmploymentTypeRecord>(query);

                employmentTypes = queriedEmploymentTypes.Select(x => new EmploymentTypeResponse(x.Id, x.Name));
                
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
            
            var queriedEmploymentType = await connection.QueryFirstOrDefaultAsync<EmploymentTypeRecord>(query, new
            {
                Id = id
            });

            if (queriedEmploymentType.Id == 0)
            {
                throw EmploymentTypeNotFoundException.ForId(id);
            }

            var employmentType = new EmploymentTypeResponse(
                queriedEmploymentType.Id,
                queriedEmploymentType.Name);

            return employmentType;
        }
    }
}