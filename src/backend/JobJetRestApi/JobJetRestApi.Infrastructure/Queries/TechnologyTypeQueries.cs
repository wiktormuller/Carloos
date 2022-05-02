using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using JobJetRestApi.Infrastructure.Configuration;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class TechnologyTypeQueries : ITechnologyTypeQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;

        private readonly record struct TechnologyTypeRecord(int Id, string Name);
        
        public TechnologyTypeQueries(ISqlConnectionFactory sqlConnectionFactory,
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
        }
    
        public async Task<IEnumerable<TechnologyTypeResponse>> GetAllTechnologyTypesAsync()
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [TechnologyType].Id,
                    [TechnologyType].Name
                FROM [TechnologyTypes] AS [TechnologyType] 
                ORDER BY [TechnologyType].Id;"
                ;

            var technologyTypes = _cacheService.Get<IEnumerable<TechnologyTypeResponse>>(CacheKeys.TechnologyTypesListKey);
                
            if (technologyTypes is null)
            {
                var queriedTechnologyTypes = await connection.QueryAsync<TechnologyTypeRecord>(query);

                technologyTypes = queriedTechnologyTypes.Select(x => new TechnologyTypeResponse(x.Id, x.Name));
                
                _cacheService.Add(technologyTypes, CacheKeys.TechnologyTypesListKey);
            }

            return technologyTypes;
        }

        public async Task<TechnologyTypeResponse> GetTechnologyTypeByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [TechnologyType].Id,
                    [TechnologyType].Name
                FROM [TechnologyTypes] AS [TechnologyType] 
                WHERE [TechnologyType].Id = @Id
                ORDER BY [TechnologyType].Id;"
                ;
            
            var queriedTechnologyType = await connection.QueryFirstOrDefaultAsync<TechnologyTypeRecord>(query, new
            {
                Id = id
            });

            if (queriedTechnologyType.Id == 0)
            {
                throw TechnologyTypeNotFoundException.ForId(id);
            }

            var technologyType = new TechnologyTypeResponse(
                queriedTechnologyType.Id, 
                queriedTechnologyType.Name);

            return technologyType;
        }
    }
}