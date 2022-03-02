using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.QueriesHandlers
{
    public class GetJobOfferByIdQueryHandler : IRequestHandler<GetJobOfferByIdQuery, JobOfferResponse>
    {
        private readonly IJobOfferRepository _jobOfferRepository;

        public GetJobOfferByIdQueryHandler(IJobOfferRepository jobOfferRepository)
        {
            _jobOfferRepository = Guard.Against.Null(jobOfferRepository, nameof(jobOfferRepository));
        }

        /// <exception cref="JobOfferNotFoundException"></exception>
        public async Task<JobOfferResponse> Handle(GetJobOfferByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _jobOfferRepository.ExistsAsync(request.Id))
            {
                throw JobOfferNotFoundException.ForId(request.Id);
            }

            var jobOffer = await _jobOfferRepository.GetByIdAsync(request.Id);
            
            var result = new JobOfferResponse(
                jobOffer.Id,
                jobOffer.Name,
                jobOffer.Description,
                jobOffer.SalaryFrom,
                jobOffer.SalaryTo,
                jobOffer.Address.Id,
                jobOffer.Address.Country.Name,
                jobOffer.Address.Town,
                jobOffer.Address.Street,
                jobOffer.Address.ZipCode,
                jobOffer.Address.Latitude,
                jobOffer.Address.Longitude,
                jobOffer.TechnologyType.Name,
                jobOffer.Seniority.Name,
                jobOffer.EmploymentType.Name,
                jobOffer.WorkSpecification);

            return result;
        }
    }
}