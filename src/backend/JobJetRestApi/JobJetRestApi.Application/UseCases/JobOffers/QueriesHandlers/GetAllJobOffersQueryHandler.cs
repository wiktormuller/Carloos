using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.QueriesHandlers
{
    public class GetAllJobOffersQueryHandler : IRequestHandler<GetAllJobOffersQuery, List<JobOfferResponse>>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        
        public GetAllJobOffersQueryHandler(IJobOfferRepository jobOfferRepository)
        {
            _jobOfferRepository = jobOfferRepository;
        }

        public async Task<List<JobOfferResponse>> Handle(GetAllJobOffersQuery request, CancellationToken cancellationToken)
        {
            var jobOffers = await _jobOfferRepository.GetAll();

            return jobOffers
                .Select(x => new JobOfferResponse(
                    x.Id, x.Name, x.Description, x.SalaryFrom, x.SalaryTo, x.Address.Id, x.Address.Country.Name,
                    x.Address.Town, x.Address.Street, x.Address.ZipCode, x.Address.Latitude, x.Address.Longitude, x.TechnologyType.Name, 
                    x.Seniority.Name, x.EmploymentType.Name))
                .ToList();
        }
    }
}