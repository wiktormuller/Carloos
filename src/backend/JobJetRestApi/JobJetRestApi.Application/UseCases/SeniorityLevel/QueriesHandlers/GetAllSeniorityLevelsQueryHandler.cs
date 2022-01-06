using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.QueriesHandlers
{
    public class GetAllSeniorityLevelsQueryHandler : IRequestHandler<GetAllSeniorityLevelsQuery, List<SeniorityLevelResponse>>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        
        public GetAllSeniorityLevelsQueryHandler(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = seniorityRepository;
        }

        public async Task<List<SeniorityLevelResponse>> Handle(GetAllSeniorityLevelsQuery request, CancellationToken cancellationToken)
        {
            var seniorityLevels = await _seniorityRepository.GetAll();

            return seniorityLevels
                .Select(x => new SeniorityLevelResponse(x.Id, x.Name))
                .ToList();
        }
    }
}