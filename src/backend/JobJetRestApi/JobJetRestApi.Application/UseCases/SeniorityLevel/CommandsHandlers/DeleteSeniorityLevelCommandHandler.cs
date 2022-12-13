using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using MediatR;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Domain.Repositories;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.CommandsHandlers
{
    public class DeleteSeniorityLevelCommandHandler : IRequestHandler<DeleteSeniorityLevelCommand>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        
        public DeleteSeniorityLevelCommandHandler(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = Guard.Against.Null(seniorityRepository, nameof(seniorityRepository));
        }
        
        public async Task<Unit> Handle(DeleteSeniorityLevelCommand request, CancellationToken cancellationToken)
        {
            if (!await _seniorityRepository.ExistsAsync(request.Id))
            {
                throw SeniorityLevelNotFoundException.ForId(request.Id);
            }

            var seniority = await _seniorityRepository.GetByIdAsync(request.Id);
            
            await _seniorityRepository.DeleteAsync(seniority);
            
            return Unit.Value;
        }
    }
}