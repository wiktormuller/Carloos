using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JetBrains.Annotations;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Profiles.Queries;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries;

public class ProfileQueries : IProfileQueries
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    private record ProfileRecord
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }

    private record CompanyRecord
    {
        public int? CompanyId { get; set; }
        [CanBeNull] public string CompanyName { get; set; }
        [CanBeNull] public string CompanyEmail { get; set; }
    }

    private record JobOfferRecord
    {
        public int? JobOfferId { get; set; }
        [CanBeNull] public string JobOfferName { get; set; }
    }

    public ProfileQueries(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
    }

    public async Task<MyProfileResponse> GetMyProfile(int currentUserId)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();

        const string query = @"
            SELECT
                U.Id AS UserId,
                U.UserName AS UserName,
                U.Email AS UserEmail,
                C.Id AS CompanyId,
                C.Name AS CompanyName,
                NULL AS CompanyEmail,
                JO.Id AS JobOfferId,
                JO.Name AS JobOfferName
            FROM AspNetUsers AS U
            LEFT JOIN Companies AS C
                ON U.Id = C.UserId
            LEFT JOIN JobOffers AS JO
                ON C.Id = JO.CompanyId
            WHERE U.Id = @UserId;
        ";

        var profileMap = new Dictionary<int, ProfileDto>();

        await connection.QueryAsync<ProfileRecord, CompanyRecord, JobOfferRecord, bool>(
            query,
            param: new { UserId = currentUserId },
            splitOn: "CompanyId,JobOfferId",
            map: (profileRecord, companyRecord, jobOfferRecord) =>
            {
                ProfileDto profileDto;

                if (!profileMap.TryGetValue(profileRecord.UserId, out profileDto))
                {
                    profileDto = new ProfileDto
                    {
                        UserId = profileRecord.UserId,
                        Name = profileRecord.UserName,
                        Email = profileRecord.UserEmail
                    };
                    profileMap.Add(profileRecord.UserId, profileDto);
                }

                if (companyRecord is not null)
                {
                    CompanyDto companyDto = profileDto.ProfileCompanies.FirstOrDefault(companyDto =>
                        companyDto.Id == companyRecord.CompanyId);
                    
                    if (companyDto is null)
                    {
                        companyDto = new CompanyDto
                        {
                            Id = companyRecord.CompanyId.Value,
                            Name = companyRecord.CompanyName,
                            Email = companyRecord.CompanyEmail
                        };
                        
                        profileDto.ProfileCompanies.Add(companyDto);
                    }
                    
                    if (jobOfferRecord is not null)
                    {
                        companyDto.CompanyJobOffers.Add(
                            new CompanyJobOfferDto
                            {
                                Id = jobOfferRecord.JobOfferId.Value,
                                Name = jobOfferRecord.JobOfferName
                            });
                    }
                }
                
                return true;
            }
        );

        var queriedProfile = profileMap.Values.FirstOrDefault();

        if (queriedProfile is null)
        {
            throw ProfileNotFoundException.ForId(currentUserId);
        }

        var profile = new MyProfileResponse(
            queriedProfile.UserId,
            queriedProfile.Name,
            queriedProfile.Email,
            queriedProfile.ProfileCompanies
                .Select(profileCompany =>
                    new ProfileCompanyResponse(
                        profileCompany.Id, 
                        profileCompany.Name, 
                        profileCompany.Email,
                        profileCompany.CompanyJobOffers
                            .Select(profileJobOffer =>
                                new CompanyJobOfferResponse(
                                    profileJobOffer.Id,
                                    profileJobOffer.Name))
                            .ToList()))
                .ToList());

        return profile;
    }
}

public class ProfileDto
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<CompanyDto> ProfileCompanies { get; set; } = new();
}

public class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<CompanyJobOfferDto> CompanyJobOffers { get; set; } = new();
}

public class CompanyJobOfferDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}