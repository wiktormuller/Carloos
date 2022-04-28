using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using JobJetRestApi.Infrastructure.Configuration;
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
    
        public async Task<IEnumerable<JobOfferResponse>> GetAllJobOffersAsync(UsersFilter usersFilter)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            var queryBuilder = new StringBuilder(@"
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
                    
                --@WHERE
                
                --@ORDERBY
                --@OFFSET
                --@FETCH;"
                );
            
            
            var parameters = BuildConditionsAndGetDynamicParameters(queryBuilder, usersFilter);
            
            var jobOffers = _cacheService.Get<IEnumerable<JobOfferResponse>>(CacheKeys.JobOffersListKey);
                
            if (jobOffers is null)
            {
                jobOffers = await connection.QueryAsync<JobOfferResponse>(queryBuilder.ToString(), parameters);

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

        public DynamicParameters BuildConditionsAndGetDynamicParameters(StringBuilder queryBuilder, UsersFilter usersFilter)
        {
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            if (usersFilter.CountryId.HasValue)
            {
                conditions.Add("[Address].CountryId = @CountryId");
                parameters.Add("CountryId", usersFilter.CountryId.Value);
            }

            if (usersFilter.TechnologyId.HasValue)
            {
                conditions.Add("[TechnologyType].Id = @TechnologyTypeId"); 
                parameters.Add("TechnologyTypeId", usersFilter.TechnologyId.Value);
            }

            if (usersFilter.EmploymentTypeId.HasValue)
            {
                conditions.Add("[EmploymentType].Id = @EmploymentTypeId");
                parameters.Add("EmploymentTypeId", usersFilter.EmploymentTypeId.Value);
            }

            if (usersFilter.SeniorityLevelId.HasValue)
            {
                conditions.Add("[Seniority].Id = @SeniorityLevelId");
                parameters.Add("SeniorityLevelId", usersFilter.SeniorityLevelId.Value);
            }

            if (! string.IsNullOrEmpty(usersFilter.WorkSpecification))
            {
                conditions.Add("[JobOffer].WorkSpecification = @WorkSpecification");
                parameters.Add("WorkSpecification", usersFilter.WorkSpecification);
            }

            if (usersFilter.SalaryFrom.HasValue)
            {
                conditions.Add("[JobOffer].SalaryFrom >= @SalaryFrom");
                parameters.Add("SalaryFrom", usersFilter.SalaryFrom.Value);
            }

            if (usersFilter.SalaryTo.HasValue)
            {
                conditions.Add("[JobOffer].SalaryTo >= @SalaryTo");
                parameters.Add("SalaryTo", usersFilter.SalaryTo.Value);
            }

            if (!string.IsNullOrEmpty(usersFilter.GeneralSearchByText))
            {
                conditions.Add(@"[JobOffer].Description = @GeneralSearchByText
                    OR [JobOffer].Name = @GeneralSearchByText");
                parameters.Add("GeneralSearchByText", usersFilter.GeneralSearchByText);
            }

            queryBuilder.Replace("--@WHERE", conditions.Any()
                ? $"WHERE {string.Join(" AND ", conditions)}" 
                : string.Empty);

            queryBuilder.Replace("--@ORDERBY", $"ORDER BY [JobOffer].Id"); // @TODO - Implement better sorting
            queryBuilder.Replace("--@OFFSET", $"OFFSET {usersFilter.PageNumber} ROWS");
            queryBuilder.Replace("--@FETCH", $"FETCH NEXT {usersFilter.PageSize} ROWS ONLY");

            return parameters;
        }
    }
}