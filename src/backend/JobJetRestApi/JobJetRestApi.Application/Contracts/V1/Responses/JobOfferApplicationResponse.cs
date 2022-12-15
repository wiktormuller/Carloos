using System;

namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class JobOfferApplicationResponse
{
    public int Id { get; private set; }
    public byte[] FileBytes { get; private set; }
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }

    public JobOfferApplicationResponse(int id, byte[] fileBytes, string fileName, string fileExtension)
    {
        Id = id;
        FileBytes = fileBytes;
        FileName = fileName;
        FileExtension = fileExtension;
    }
}