using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.Queries
{
    public class GetAllCompaniesQuery : IRequest<List<CompanyResponse>>
    {
        public GetAllCompaniesQuery()
        {
            
        }
    }
}