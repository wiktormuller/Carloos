using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using JobJetRestApi.Infrastructure.Factories;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class JobOfferQueries : IJobOfferQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMemoryCache _memoryCache;
        
        public JobOfferQueries(ISqlConnectionFactory sqlConnectionFactory, 
            IMemoryCache memoryCache)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
        }
    
        public async Task<IEnumerable<JobOfferResponse>> GetAllJobOffersAsync(PaginationFilter paginationFilter)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string query = @"
                SELECT 
                    [JobOffer].Id,
                    [JobOffer].Name,
                    [JobOffer].Description,
                    [JobOffer].SalaryFrom,
                    [JobOffer].SalaryTo,
                    [JobOffer].SalaryTo,
                    [JobOffer].WorkSpecification,
                    [Address].Id,
                    [Address].Town,
                    [Address].Street,
                    [Address].ZipCode,
                    [Address].Latitude,
                    [Address].Longitude,
                    [TechnologyType].Id,
                    [TechnologyType].Name,
                    [Seniority].Id,
                    [Seniority].Name,
                    [EmploymentType].Id,
                    [EmploymentType].Name 
                FROM [JobOffers] AS [JobOffer] 
                LEFT JOIN Addresses AS [Address]
                    ON [JobOffers].AddressId = [Addresses].Id
                LEFT JOIN TechnologyTypes AS [TechnologyType]
                    ON [JobOffers].TechnologyTypeId = [TechnologyTypes].Id
                LEFT JOIN SeniorityLevels AS [Seniority]
                    ON [JobOffers].SeniorityId = [SeniorityLevels].Id
                LEFT JOIN EmploymentTypes AS [EmploymentType]
                    ON [JobOffers].EmploymentTypeId = [SeniorityLevels].Id
                
                ORDER BY [JobOffer].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
                ;
            
            var cacheKey = "jobOffersKey";
            
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<JobOfferResponse> jobOffers))
            {
                jobOffers = await connection.QueryAsync<JobOfferResponse>(query, new
                {
                    OffsetRows = paginationFilter.PageNumber,
                    FetchRows = paginationFilter.PageSize
                });
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
            
                _memoryCache.Set(cacheKey, jobOffers, cacheExpiryOptions);
            }

            return jobOffers;
        }
    }
}