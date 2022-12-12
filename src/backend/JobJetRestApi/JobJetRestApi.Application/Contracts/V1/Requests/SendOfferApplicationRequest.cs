using Microsoft.AspNetCore.Http;

namespace JobJetRestApi.Application.Contracts.V1.Requests;

public class SendOfferApplicationRequest
{
    public string UserEmail { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile File { get; set; }
}