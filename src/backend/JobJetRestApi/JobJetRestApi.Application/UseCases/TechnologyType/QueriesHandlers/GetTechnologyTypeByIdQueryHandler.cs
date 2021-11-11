using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.QueriesHandlers
{
    public class GetTechnologyTypeByIdQueryHandler : IRequestHandler<GetTechnologyTypeByIdQuery, TechnologyTypeResponse>
    {
        public GetTechnologyTypeByIdQueryHandler()
        {
            
        }
        
        public Task<TechnologyTypeResponse> Handle(GetTechnologyTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new TechnologyTypeResponse();

            return Task.FromResult(result);
        }
    }
}