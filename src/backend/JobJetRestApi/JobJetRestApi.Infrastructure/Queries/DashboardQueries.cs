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

    private readonly record struct AverageSalaryForTechnologiesRecord(int TechnologyTypeId, string TechnologyTypeName, 
        decimal AverageSalaryFrom, decimal AverageSalaryTo, decimal AverageSalary);

    private readonly record struct AverageSalaryForCountriesRecord(int CountryId, string CountryName,
        decimal AverageSalaryFrom, decimal AverageSalaryTo, decimal AverageSalary);

    private readonly record struct AverageSalaryForSeniorityLevelsRecord(int SeniorityLevelId,
        string SeniorityLevelName,
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
            var queriedAverageSalaries = await connection.QueryAsync<AverageSalaryForTechnologiesRecord>(query);

            averageSalariesForTechnologies = queriedAverageSalaries.Select(x =>
                new AverageSalaryForTechnologiesResponse
                    (x.TechnologyTypeId, x.TechnologyTypeName, x.AverageSalaryFrom, x.AverageSalaryTo, x.AverageSalary));
                
            _cacheService.Add(averageSalariesForTechnologies, CacheKeys.AverageSalariesForTechnologiesListKey);
        }

        return averageSalariesForTechnologies;
    }
    
    public async Task<IEnumerable<AverageSalaryForCountriesResponse>> GetAverageSalariesForCountries()
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();
        
        const string query = @"
            SELECT 
                   C.Id AS CountryId, 
                   C.Name AS CountryName, 
                   AVG(JO.SalaryFrom) AS AverageSalaryFrom,
                   AVG(JO.SalaryTo) AS AverageSalaryTo,
                   ((AVG(JO.SalaryFrom) + AVG(JO.SalaryTo)) / 2) AS AverageSalary
            FROM [JobOffers] AS JO
                JOIN [Address] AS A
                    ON JO.AddressId = A.Id
                JOIN [Countries] AS C
                    ON A.CountryId = C.Id
            GROUP BY C.Name, C.Id;
        ";
            
        var averageSalariesForCountries = _cacheService
            .Get<IEnumerable<AverageSalaryForCountriesResponse>>(CacheKeys.AverageSalariesForCountriesListKey);
                
        if (averageSalariesForCountries is null)
        {
            var queriedAverageSalaries = await connection.QueryAsync<AverageSalaryForCountriesRecord>(query);

            averageSalariesForCountries = queriedAverageSalaries.Select(x =>
                new AverageSalaryForCountriesResponse
                    (x.CountryId, x.CountryName, x.AverageSalaryFrom, x.AverageSalaryTo, x.AverageSalary));
                
            _cacheService.Add(averageSalariesForCountries, CacheKeys.AverageSalariesForCountriesListKey);
        }

        return averageSalariesForCountries;
    }
    
    public async Task<IEnumerable<AverageSalaryForSeniorityLevelsResponse>> GetAverageSalariesForSeniorityLevels()
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();
        
        const string query = @"
            SELECT 
                   SL.Id AS SeniorityLevelid, 
                   SL.Name AS SeniorityLevelName, 
                   AVG(JO.SalaryFrom) AS AverageSalaryFrom,
                   AVG(JO.SalaryTo) AS AverageSalaryTo,
                   ((AVG(JO.SalaryFrom) + AVG(JO.SalaryTo)) / 2) AS AverageSalary
            FROM [JobOffers] AS JO
                JOIN [SeniorityLevels] AS SL
                    ON JO.SeniorityId = SL.Id
            GROUP BY SL.Name, SL.Id;
        ";
            
        var averageSalariesForSeniorityLevels = _cacheService
            .Get<IEnumerable<AverageSalaryForSeniorityLevelsResponse>>(CacheKeys.AverageSalariesForSeniorityLevelsListKey);
                
        if (averageSalariesForSeniorityLevels is null)
        {
            var queriedAverageSalaries = await connection.QueryAsync<AverageSalaryForSeniorityLevelsRecord>(query);

            averageSalariesForSeniorityLevels = queriedAverageSalaries.Select(x =>
                new AverageSalaryForSeniorityLevelsResponse
                    (x.SeniorityLevelId, x.SeniorityLevelName, x.AverageSalaryFrom, x.AverageSalaryTo, x.AverageSalary));
                
            _cacheService.Add(averageSalariesForSeniorityLevels, CacheKeys.AverageSalariesForCountriesListKey);
        }

        return averageSalariesForSeniorityLevels;
    }
}