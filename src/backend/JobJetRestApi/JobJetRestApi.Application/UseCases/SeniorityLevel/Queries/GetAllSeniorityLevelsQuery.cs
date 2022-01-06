using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.Queries
{
    public class GetAllSeniorityLevelsQuery : IRequest<List<SeniorityLevelResponse>>
    {
        
    }
}