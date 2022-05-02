using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using JobJetRestApi.Infrastructure.Configuration;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class SeniorityLevelQueries : ISeniorityLevelQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;

        private readonly record struct SeniorityLevelRecord(int Id, string Name);
        
        public SeniorityLevelQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
        }
    
        public async Task<IEnumerable<SeniorityLevelResponse>> GetAllSeniorityLevelsAsync()
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [SeniorityLevel].Id,
                    [SeniorityLevel].Name
                FROM [SeniorityLevels] AS [SeniorityLevel] 
                ORDER BY [SeniorityLevel].Id;"
                ;
            
            var seniorityLevels = _cacheService.Get<IEnumerable<SeniorityLevelResponse>>(CacheKeys.SeniorityTypesListKey);
                
            if (seniorityLevels is null)
            {
                var queriedSeniorityLevels = await connection.QueryAsync<SeniorityLevelRecord>(query);

                seniorityLevels = queriedSeniorityLevels.Select(x => 
                    new SeniorityLevelResponse(x.Id, x.Name));
                
                _cacheService.Add(seniorityLevels, CacheKeys.SeniorityTypesListKey);
            }

            return seniorityLevels;
        }

        public async Task<SeniorityLevelResponse> GetSeniorityLevelByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [SeniorityLevel].Id,
                    [SeniorityLevel].Name
                FROM [SeniorityLevels] AS [SeniorityLevel] 
                WHERE [SeniorityLevel.Id] = @Id
                ORDER BY [SeniorityLevel].Id;"
                ;
            
            var queriedSeniorityLevel = await connection.QueryFirstOrDefaultAsync<SeniorityLevelRecord>(query, new
            {
                Id = id
            });

            if (queriedSeniorityLevel.Id == 0)
            {
                throw SeniorityLevelNotFoundException.ForId(id);
            }

            var seniorityLevel = new SeniorityLevelResponse(
                queriedSeniorityLevel.Id, 
                queriedSeniorityLevel.Name);

            return seniorityLevel;
        }
    }
}