using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.Queries
{
    public class GetTechnologyTypeByIdQuery : IRequest<TechnologyTypeResponse>
    {
        public int Id { get; private set; }
        
        public GetTechnologyTypeByIdQuery(int id)
        {
            Id = id;
        }
    }
}