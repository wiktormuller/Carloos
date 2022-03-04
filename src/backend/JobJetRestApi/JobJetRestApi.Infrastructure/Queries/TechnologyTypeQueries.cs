using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Common.Config;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class TechnologyTypeQueries : ITechnologyTypeQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;
        
        public TechnologyTypeQueries(ISqlConnectionFactory sqlConnectionFactory,
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
        }
    
        public async Task<IEnumerable<TechnologyTypeResponse>> GetAllTechnologyTypesAsync(PaginationFilter paginationFilter)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [TechnologyType].Id,
                    [TechnologyType].Name
                FROM [TechnologyTypes] AS [TechnologyType] 
                ORDER BY [TechnologyType].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
                ;
            
            var technologyTypes = _cacheService.Get<IEnumerable<TechnologyTypeResponse>>(CacheKeys.TechnologyTypesListKey);
                
            if (technologyTypes is null)
            {
                technologyTypes = await connection.QueryAsync<TechnologyTypeResponse>(query, new
                {
                    OffsetRows = paginationFilter.PageNumber,
                    FetchRows = paginationFilter.PageSize
                });

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
            
            var technologyType = await connection.QueryFirstOrDefaultAsync<TechnologyTypeResponse>(query, new
            {
                Id = id
            });

            if (technologyType is null)
            {
                throw TechnologyTypeNotFoundException.ForId(id);
            }

            return technologyType;
        }
    }
}