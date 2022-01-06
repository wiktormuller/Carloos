using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.Queries
{
    public class GetAllJobOffersQuery : IRequest<List<JobOfferResponse>>
    {
        
    }
}