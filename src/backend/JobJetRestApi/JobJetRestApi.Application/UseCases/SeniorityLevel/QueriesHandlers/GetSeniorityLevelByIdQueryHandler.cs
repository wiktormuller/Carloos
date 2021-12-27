using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.QueriesHandlers
{
    public class GetSeniorityLevelByIdQueryHandler : IRequestHandler<GetSeniorityLevelByIdQuery, SeniorityLevelResponse>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        
        public GetSeniorityLevelByIdQueryHandler(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = seniorityRepository;
        }
        
        public async Task<SeniorityLevelResponse> Handle(GetSeniorityLevelByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _seniorityRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            var seniorityLevel = await _seniorityRepository.GetById(request.Id);
            
            var result = new SeniorityLevelResponse(seniorityLevel.Id, seniorityLevel.Name);

            return result;
        }
    }
}