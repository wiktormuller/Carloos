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

    private readonly record struct AverageSalaryAndMedianRecord(int CountryId, decimal AverageSalary, decimal MedianSalary);
    
    public DashboardQueries(ISqlConnectionFactory sqlConnectionFactory, 
        ICacheService cacheService)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _cacheService = cacheService;
    }
    
    public async Task<IEnumerable<AverageSalaryInCountryResponse>> GetAverageAndMedianSalariesInCountries()
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();
        
        // @TODO - Implement query
        const string query = @"
            
        ";
            
        var averageAndMedianSalaries = _cacheService.Get<IEnumerable<AverageSalaryInCountryResponse>>(CacheKeys.AverageAndMedianSalariesListKey);
                
        if (averageAndMedianSalaries is null)
        {
            var queriedAverageAndMedianSalaries = await connection.QueryAsync<AverageSalaryAndMedianRecord>(query);

            averageAndMedianSalaries = queriedAverageAndMedianSalaries.Select(x =>
                new AverageSalaryInCountryResponse(x.CountryId, x.AverageSalary, x.MedianSalary));
                
            _cacheService.Add(averageAndMedianSalaries, CacheKeys.AverageAndMedianSalariesListKey);
        }

        return averageAndMedianSalaries;
    }
}