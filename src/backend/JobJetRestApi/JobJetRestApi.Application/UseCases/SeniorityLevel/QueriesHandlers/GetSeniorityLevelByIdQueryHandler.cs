using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.QueriesHandlers
{
    public class GetSeniorityLevelByIdQueryHandler : IRequestHandler<GetSeniorityLevelByIdQuery, SeniorityLevelResponse>
    {
        public GetSeniorityLevelByIdQueryHandler()
        {
            
        }
        
        public Task<SeniorityLevelResponse> Handle(GetSeniorityLevelByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new SeniorityLevelResponse();

            return Task.FromResult(result);
        }
    }
}