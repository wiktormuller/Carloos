using System;

namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class JobOfferApplicationResponse
{
    public int Id { get; private set; }
    public string UserEmail { get; private set; }
    public string PhoneNumber { get; private set; }
    public string FileName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int JobOfferId { get; private set; }
    
    public JobOfferApplicationResponse(int id, string userEmail, string phoneNumber, 
        string fileName, DateTime createdAt, int jobOfferId)
    {
        Id = id;
        UserEmail = userEmail;
        PhoneNumber = phoneNumber;
        FileName = fileName;
        CreatedAt = createdAt;
        JobOfferId = jobOfferId;
    }
}