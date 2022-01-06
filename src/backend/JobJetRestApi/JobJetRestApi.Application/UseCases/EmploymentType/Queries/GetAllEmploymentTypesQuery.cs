using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.Queries
{
    public class GetAllEmploymentTypesQuery : IRequest<List<EmploymentTypeResponse>>
    {
        
    }
}