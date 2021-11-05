using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.QueriesHandlers
{
    public class GetJobOfferByIdQueryHandler : IRequestHandler<GetJobOfferByIdQuery, JobOfferResponse>
    {
        public Task<JobOfferResponse> Handle(GetJobOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new JobOfferResponse();

            return Task.FromResult(result);
        }
    }
}