using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.JobOfferApplications.Queries;
using JobJetRestApi.Domain.Repositories;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries;

public class JobOfferApplicationQueries : IJobOfferApplicationQueries
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IJobOfferRepository _jobOfferRepository;
    private readonly IUserRepository _userRepository;

    private readonly record struct CompanyRecord(int Id, string Name);

    private readonly record struct JobOfferRecord(int Id, string Name);
    
    private record JobOfferApplicationDto(int Id, string UserEmail, string PhoneNumber, 
        string FileName, string FileExtension, byte[] FileBytes,
        DateTime CreatedAt, DateTime UpdatedAt, int JobOfferId);
    
    public JobOfferApplicationQueries(ISqlConnectionFactory sqlConnectionFactory, 
        IJobOfferRepository jobOfferRepository, 
        IUserRepository userRepository)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _jobOfferRepository = jobOfferRepository;
        _userRepository = userRepository;
    }

    public async Task<JobOfferApplicationResponse> GetJobOfferApplicationByIdAsync(int jobOfferId, int jobOfferApplicationId, int currentUserId)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();

        const string queryUserCompanies = @"
            SELECT
                [Company].Id,
                [Company].Name
            FROM [Companies] AS Company
            WHERE [Company].UserId = @UserId;
        ";
        
        var queriedUserCompanies = await connection.QueryAsync<CompanyRecord>(queryUserCompanies, new
        {
            UserId = currentUserId
        });

        const string queryJobOffers = @"
            SELECT
                [JobOffer].Id,
                [JobOffer].Name
            FROM [JobOffers] AS JobOffer
            WHERE [JobOffer].CompanyId IN @CompaniesIds;
        ";

        var queriedJobOffers = await connection.QueryAsync<JobOfferRecord>(queryJobOffers, new
        {
            CompaniesIds = queriedUserCompanies.Select(x => x.Id).ToList()
        });

        if (queriedJobOffers.All(x => x.Id != jobOfferId))
        {
            throw JobOfferNotFoundException.ForId(jobOfferId);
        }

        const string queryJobApplication = @"
                SELECT 
                    [JobOfferApplication].Id,
                    [JobOfferApplication].UserEmail,
                    [JobOfferApplication].PhoneNumber,
                    [JobOfferApplication].FileName,
                    [JobOfferApplication].FileExtension,
                    [JobOfferApplication].FileBytes,
                    [JobOfferApplication].CreatedAt,
                    [JobOfferApplication].UpdatedAt,
                    [JobOfferApplication].JobOfferId
                FROM [JobOfferApplications] AS [JobOfferApplication] 
                WHERE [JobOfferApplication].Id = @Id
                    AND [JobOfferApplication].JobOfferId = @JobOfferId
                ORDER BY [JobOfferApplication].Id;"
            ;
            
        var queriedJobOfferApplication = await connection.QueryFirstOrDefaultAsync<JobOfferApplicationDto>(queryJobApplication, new
        {
            Id = jobOfferApplicationId,
            JobOfferId = jobOfferId
        });
            
        if (queriedJobOfferApplication is null)
        {
            throw JobOfferApplicationNotFoundException.ForId(jobOfferApplicationId);
        }

        return new JobOfferApplicationResponse(
            queriedJobOfferApplication.Id,
            queriedJobOfferApplication.FileBytes,
            queriedJobOfferApplication.FileName,
            queriedJobOfferApplication.FileExtension);
    }
}