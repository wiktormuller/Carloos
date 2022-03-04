using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Common.Config;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class JobOfferQueries : IJobOfferQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICacheService _cacheService;
        
        public JobOfferQueries(ISqlConnectionFactory sqlConnectionFactory, 
            ICacheService cacheService)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
            _cacheService = Guard.Against.Null(cacheService, nameof(cacheService));
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
            
            var jobOffers = _cacheService.Get<IEnumerable<JobOfferResponse>>(CacheKeys.JobOffersListKey);
                
            if (jobOffers is null)
            {
                jobOffers = await connection.QueryAsync<JobOfferResponse>(query, new
                {
                    OffsetRows = paginationFilter.PageNumber,
                    FetchRows = paginationFilter.PageSize
                });

                _cacheService.Add(jobOffers, CacheKeys.JobOffersListKey);
            }

            return jobOffers;
        }

        /// <exception cref="JobOfferNotFoundException"></exception>
        public async Task<JobOfferResponse> GetJobOfferByIdAsync(int id)
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
            
            var jobOffer = await connection.QueryFirstOrDefaultAsync<JobOfferResponse>(query, new
            {
                Id = id
            });

            if (jobOffer is null)
            {
                throw JobOfferNotFoundException.ForId(id);
            }

            return jobOffer;
        }
    }
}