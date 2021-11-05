using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.Queries
{
    public class GetJobOfferByIdQuery : IRequest<JobOfferResponse>
    {
        public int Id { get; }

        public GetJobOfferByIdQuery(int id)
        {
            Id = id;
        }
    }
}