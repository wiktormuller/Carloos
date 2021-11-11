using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.Queries
{
    public class GetSeniorityLevelByIdQuery : IRequest<SeniorityLevelResponse>
    {
        public int Id { get; private set; }
        
        public GetSeniorityLevelByIdQuery(int id)
        {
            Id = id;
        }
    }
}