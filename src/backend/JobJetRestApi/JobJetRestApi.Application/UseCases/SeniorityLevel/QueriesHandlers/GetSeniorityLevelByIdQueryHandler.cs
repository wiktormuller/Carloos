using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using MediatR;
using JobJetRestApi.Application.Exceptions;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.QueriesHandlers
{
    public class GetSeniorityLevelByIdQueryHandler : IRequestHandler<GetSeniorityLevelByIdQuery, SeniorityLevelResponse>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        
        public GetSeniorityLevelByIdQueryHandler(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = Guard.Against.Null(seniorityRepository, nameof(seniorityRepository));
        }
        
        /// <exception cref="SeniorityLevelNotFoundException"></exception>
        public async Task<SeniorityLevelResponse> Handle(GetSeniorityLevelByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _seniorityRepository.ExistsAsync(request.Id))
            {
                throw SeniorityLevelNotFoundException.ForId(request.Id);
            }

            var seniorityLevel = await _seniorityRepository.GetByIdAsync(request.Id);
            
            var result = new SeniorityLevelResponse(seniorityLevel.Id, seniorityLevel.Name);

            return result;
        }
    }
}