using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobJetRestApi.Application.UseCases.JobOffers.Commands;

public class SendOfferApplicationCommand : IRequest
{
    public int JobOfferId { get; }
    public string UserEmail { get; }
    public string PhoneNumber { get; }
    public IFormFile File { get; }
    
    public SendOfferApplicationCommand(int jobOfferId, string userEmail, string phoneNumber, IFormFile file)
    {
        JobOfferId = jobOfferId;
        UserEmail = userEmail;
        PhoneNumber = phoneNumber;
        File = file;
    }
}