using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Dashboards.Queries;
using JobJetRestApi.Infrastructure.Configuration;
using JobJetRestApi.Infrastructure.Factories;
using System.Linq;

namespace JobJetRestApi.Infrastructure.Queries;

public class DashboardQueries : IDashboardQueries
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ICacheService _cacheService;

    private readonly record struct AverageSalaryAndMedianRecord(int TechnologyTypeId, string TechnologyTypeName, 
        decimal AverageSalaryFrom, decimal AverageSalaryTo, decimal AverageSalary);
    
    public DashboardQueries(ISqlConnectionFactory sqlConnectionFactory, 
        ICacheService cacheService)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _cacheService = cacheService;
    }
    
    public async Task<IEnumerable<AverageSalaryForTechnologiesResponse>> GetAverageSalariesForTechnologies()
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();
        
        const string query = @"
            SELECT 
                   TT.Id AS TechnologyTypeId, 
                   TT.Name AS TechnologyTypeName, 
                   AVG(JO.SalaryFrom) AS AverageSalaryFrom, 
                   AVG(JO.SalaryTo) AS AverageSalaryTo, 
                   ((AVG(JO.SalaryFrom) + AVG(JO.SalaryTo)) / 2) AS AverageSalary
            FROM [TechnologyTypes] AS TT
                     JOIN [JobOfferTechnologyType] AS JOTT
                          ON TT.Id = JOTT.TechnologyTypesId
                     JOIN [JobOffers] AS JO
                          ON JOTT.JobOffersId = JO.Id
            GROUP BY TT.Name, TT.Id
        ";
            
        var averageSalariesForTechnologies = _cacheService
            .Get<IEnumerable<AverageSalaryForTechnologiesResponse>>(CacheKeys.AverageSalariesForTechnologiesListKey);
                
        if (averageSalariesForTechnologies is null)
        {
            var queriedAverageSalaries = await connection.QueryAsync<AverageSalaryAndMedianRecord>(query);

            averageSalariesForTechnologies = queriedAverageSalaries.Select(x =>
                new AverageSalaryForTechnologiesResponse
                    (x.TechnologyTypeId, x.TechnologyTypeName, x.AverageSalaryFrom, x.AverageSalaryTo, x.AverageSalary));
                
            _cacheService.Add(averageSalariesForTechnologies, CacheKeys.AverageSalariesForTechnologiesListKey);
        }

        return averageSalariesForTechnologies;
    }
}