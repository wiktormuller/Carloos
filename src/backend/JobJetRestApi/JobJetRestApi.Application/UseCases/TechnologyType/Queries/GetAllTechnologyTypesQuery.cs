using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.Queries
{
    public class GetAllTechnologyTypesQuery : IRequest<List<TechnologyTypeResponse>>
    {
        
    }
}