using System.Collections.Generic;

namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class MyProfileResponse
{
    public int UserId { get; }
    public string Name { get; }
    public string Email { get; }
    public List<ProfileCompanyResponse> ProfileCompanies { get; } = new();

    public MyProfileResponse(int userId, string name, string email, List<ProfileCompanyResponse> profileCompanies)
    {
        UserId = userId;
        Name = name;
        Email = email;
        ProfileCompanies = profileCompanies;
    }
}

public class ProfileCompanyResponse
{
    public int CompanyId { get; }
    public string Name { get; }
    public string Email { get; }
    public List<CompanyJobOfferResponse> CompanyJobOffers { get; } = new();

    public ProfileCompanyResponse(int companyId, string name, string email, List<CompanyJobOfferResponse> companyJobOffers)
    {
        CompanyId = companyId;
        Name = name;
        Email = email;
        CompanyJobOffers = companyJobOffers;
    }
}

public class CompanyJobOfferResponse
{
    public int JobOfferId { get; }
    public string Name { get; }

    public CompanyJobOfferResponse(int jobOfferId, string name)
    {
        JobOfferId = jobOfferId;
        Name = name;
    }
}