using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Dapper;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Profiles.Queries;
using JobJetRestApi.Infrastructure.Factories;

namespace JobJetRestApi.Infrastructure.Queries;

public class ProfileQueries : IProfileQueries
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    private readonly record struct ProfileRecord(int UserId, string UserName, string UserEmail);

    private readonly record struct CompanyRecord(int CompanyId, string CompanyName, string CompanyEmail);

    private readonly record struct JobOfferRecord(int JobOfferId, string JobOfferName);

    public ProfileQueries(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = Guard.Against.Null(sqlConnectionFactory, nameof(sqlConnectionFactory));
    }

    public async Task<MyProfileResponse> GetMyProfile(int currentUserId)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();

        const string query = @"
            
        ";

        var profileMap = new Dictionary<int, ProfileDto>();

        await connection.QueryAsync<ProfileRecord, CompanyRecord, JobOfferRecord, bool>(
            query,
            param: new { UserId = currentUserId },
            splitOn: "CompanyId,JobOfferId",
            map: (profileRecord, companyRecord, JobOfferRecord) =>
            {
                // @TOOD - Implement the mapper
                
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