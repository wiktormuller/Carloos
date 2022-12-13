using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Extensions;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers;

public class SendOfferApplicationCommandHandler : IRequestHandler<SendOfferApplicationCommand>
{
    private readonly IJobOfferRepository _jobOfferRepository;
    private readonly IJobOfferApplicationRepository _jobOfferApplicationRepository;
    
    public SendOfferApplicationCommandHandler(IJobOfferRepository jobOfferRepository,
        IJobOfferApplicationRepository jobOfferApplicationRepository)
    {
        _jobOfferRepository = jobOfferRepository;
        _jobOfferApplicationRepository = jobOfferApplicationRepository;
    }
    
    /// <exception cref="JobOfferNotFoundException"></exception>
    public async Task<Unit> Handle(SendOfferApplicationCommand request, CancellationToken cancellationToken)
    {
        var jobOffer = await _jobOfferRepository.GetByIdAsync(request.JobOfferId);

        if (jobOffer is null)
        {
            throw JobOfferNotFoundException.ForId(request.JobOfferId);
        }

        var fileExtension = Path.GetExtension(request.File.FileName).ToLowerInvariant();
        var fileBytes = await request.File.GetBytes();
        var fileName = Guid.NewGuid().ToString();

        var jobOfferApplication = new JobOfferApplication(
            request.UserEmail,
            request.PhoneNumber,
            fileName,
            fileExtension,
            fileBytes,
            jobOffer
        );

        await _jobOfferApplicationRepository.CreateAsync(jobOfferApplication);

        return await Task.FromResult(Unit.Value);
    }
}