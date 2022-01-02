using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.QueriesHandlers
{
    public class GetJobOfferByIdQueryHandler : IRequestHandler<GetJobOfferByIdQuery, JobOfferResponse>
    {
        private readonly IJobOfferRepository _jobOfferRepository;

        public GetJobOfferByIdQueryHandler(IJobOfferRepository jobOfferRepository)
        {
            _jobOfferRepository = jobOfferRepository;
        }

        public async Task<JobOfferResponse> Handle(GetJobOfferByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _jobOfferRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @ TODO - Throw Domain Exception
            }

            var jobOffer = await _jobOfferRepository.GetById(request.Id);
            
            var result = new JobOfferResponse(
                jobOffer.Id,
                jobOffer.Name,
                jobOffer.Description,
                jobOffer.SalaryFrom,
                jobOffer.SalaryTo,
                jobOffer.Address.Id,
                jobOffer.Address.Country.Name,
                jobOffer.Address.Town,
                jobOffer.Address.ZipCode,
                jobOffer.Address.Latitude,
                jobOffer.Address.Longitude,
                jobOffer.TechnologyType.Name,
                jobOffer.Seniority.Name,
                jobOffer.EmploymentType.Name);

            return result;
        }
    }
}