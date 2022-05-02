using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using JobJetRestApi.Domain.Enums;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries
{
    public class JobOfferQueries : IJobOfferQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        
        private readonly record struct JobOfferRecord(int Id, string Name, string Description, decimal SalaryFrom, decimal SalaryTo,
            string WorkSpecification, int AddressId, string Town, string Street, string ZipCode, string CountryName, decimal Latitude, decimal Longitude,
            int TechnologyTypeId, string TechnologyTypeName, int SeniorityId, string SeniorityName, int EmploymentTypeId, string EmploymentTypeName);
        
        public JobOfferQueries(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
        }
    
        public async Task<IEnumerable<JobOfferResponse>> GetAllJobOffersAsync(JobOffersFilter usersFilter)
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
                    [Address].Id AS AddressId,
                    [Address].Town,
                    [Address].Street,
                    [Address].ZipCode,
                    [Country].Name AS CountryName,
                    [Address].Latitude,
                    [Address].Longitude,
                    [TechnologyType].Id AS TechnologyTypeId,
                    [TechnologyType].Name AS TechnologyTypeName,
                    [Seniority].Id AS SeniorityId,
                    [Seniority].Name AS SeniorityName,
                    [EmploymentType].Id AS EmploymentTypeId,
                    [EmploymentType].Name AS EmploymentTypeName
                FROM [JobOffers] AS [JobOffer] 
                LEFT JOIN Addresses AS [Address]
                    ON [JobOffer].AddressId = [Address].Id
                INNER JOIN Countries AS [Country]
                    ON [Address].CountryId = [Country].Id
                LEFT JOIN TechnologyTypes AS [TechnologyType]
                    ON [JobOffer].TechnologyTypeId = [TechnologyType].Id
                LEFT JOIN SeniorityLevels AS [Seniority]
                    ON [JobOffer].SeniorityId = [Seniority].Id
                LEFT JOIN EmploymentTypes AS [EmploymentType]
                    ON [JobOffer].EmploymentTypeId = [Seniority].Id
                    
                --@WHERE
                
                --@ORDERBY
                --@OFFSET
                --@FETCH;"
                );
            
            
            var parameters = BuildConditionsAndGetDynamicParameters(queryBuilder, usersFilter);

            var queriedJobOffers = await connection.QueryAsync<JobOfferRecord>(queryBuilder.ToString(), parameters);

            var jobOffers = queriedJobOffers.Select(x => new JobOfferResponse(
                x.Id,
                x.Name,
                x.Description,
                x.SalaryFrom,
                x.SalaryTo,
                x.AddressId,
                x.CountryName,
                x.Town,
                x.Street,
                x.ZipCode,
                x.Latitude,
                x.Longitude,
                x.TechnologyTypeName,
                x.SeniorityName,
                x.EmploymentTypeName,
                Enum.Parse<WorkSpecification>(x.WorkSpecification)
                ));
            
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
                    [Address].Id AS AddressId,
                    [Address].Town,
                    [Address].Street,
                    [Address].ZipCode,
                    [Country].Name AS CountryName,
                    [Address].Latitude,
                    [Address].Longitude,
                    [TechnologyType].Id AS TechnologyTypeId,
                    [TechnologyType].Name AS TechnologyTypeName,
                    [Seniority].Id AS SeniorityId,
                    [Seniority].Name AS SeniorityName,
                    [EmploymentType].Id AS EmploymentTypeId,
                    [EmploymentType].Name AS EmploymentTypeName
                FROM [JobOffers] AS [JobOffer] 
                LEFT JOIN Addresses AS [Address]
                    ON [JobOffer].AddressId = [Address].Id
                INNER JOIN Countries AS [Country]
                    ON [Address].CountryId = [Country].Id
                LEFT JOIN TechnologyTypes AS [TechnologyType]
                    ON [JobOffer].TechnologyTypeId = [TechnologyType].Id
                LEFT JOIN SeniorityLevels AS [Seniority]
                    ON [JobOffer].SeniorityId = [Seniority].Id
                LEFT JOIN EmploymentTypes AS [EmploymentType]
                    ON [JobOffer].EmploymentTypeId = [Seniority].Id
                
                ORDER BY [JobOffer].Id 
                OFFSET @OffsetRows ROWS
                FETCH NEXT @FetchRows ROWS ONLY;"
                ;
            
            var queriedJobOffer = await connection.QueryFirstOrDefaultAsync<JobOfferRecord>(query, new
            {
                Id = id
            });

            if (queriedJobOffer.Id == 0)
            {
                throw JobOfferNotFoundException.ForId(id);
            }

            var jobOffer = new JobOfferResponse(
                queriedJobOffer.Id,
                queriedJobOffer.Name,
                queriedJobOffer.Description,
                queriedJobOffer.SalaryFrom,
                queriedJobOffer.SalaryTo,
                queriedJobOffer.AddressId,
                queriedJobOffer.CountryName,
                queriedJobOffer.Town,
                queriedJobOffer.Street,
                queriedJobOffer.ZipCode,
                queriedJobOffer.Latitude,
                queriedJobOffer.Longitude,
                queriedJobOffer.TechnologyTypeName,
                queriedJobOffer.SeniorityName,
                queriedJobOffer.EmploymentTypeName,
                Enum.Parse<WorkSpecification>(queriedJobOffer.WorkSpecification)
                );

            return jobOffer;
        }

        public DynamicParameters BuildConditionsAndGetDynamicParameters(StringBuilder queryBuilder, JobOffersFilter usersFilter)
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
            queryBuilder.Replace("--@FETCH", $"FETCH NEXT {usersFilter.GetNormalizedPageSize()} ROWS ONLY");

            return parameters;
        }
    }
}