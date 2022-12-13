using System;

namespace JobJetRestApi.Domain.Entities;

public class JobOfferApplication
{
    public int Id { get; private set; }
    public string UserEmail { get; private set; }
    public string PhoneNumber { get; private set; }
    
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }
    public byte[] FileBytes { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public int JobOfferId { get; private set; }
    public JobOffer JobOffer { get; private set; }

    private JobOfferApplication()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public JobOfferApplication(
        string userEmail,
        string phoneNumber,
        string fileName,
        string fileExtension,
        byte[] fileBytes,
        JobOffer jobOffer)
    {
        UserEmail = userEmail;
        PhoneNumber = phoneNumber;
        FileName = fileName;
        FileExtension = fileExtension;
        FileBytes = fileBytes;
        JobOffer = jobOffer;
    }
}