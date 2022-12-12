using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Extensions;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers;

public class SendOfferApplicationCommandHandler : IRequestHandler<SendOfferApplicationCommand>
{
    private readonly IJobOfferRepository _jobOfferRepository;
    
    public SendOfferApplicationCommandHandler(IJobOfferRepository jobOfferRepository)
    {
        _jobOfferRepository = jobOfferRepository;
    }
    
    /// <exception cref="JobOfferNotFoundException"></exception>
    public async Task<Unit> Handle(SendOfferApplicationCommand request, CancellationToken cancellationToken)
    {
        var jobOffer = await _jobOfferRepository.GetByIdAsync(request.JobOfferId);

        if (jobOffer is null)
        {
            throw JobOfferNotFoundException.ForId(request.JobOfferId);
        }

        var userEmail = request.UserEmail;
        var phoneNumber = request.PhoneNumber;
        var jobOfferId = request.JobOfferId;
        var bytes = await request.File.GetBytes();
        var bytesAsBase64String = bytes.GetBase64String();
        
        // TODO: Save file with manually generated filename

        return await Task.FromResult(Unit.Value);
    }
}