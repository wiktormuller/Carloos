using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.Queries
{
    public class GetCompanyByIdQuery : IRequest<CompanyResponse>
    {
        public int Id { get; private set; }
        
        public GetCompanyByIdQuery(int id)
        {
            Id = id;
        }
    }
}