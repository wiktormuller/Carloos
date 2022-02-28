using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using MediatR;
using JobJetRestApi.Application.Exceptions;

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
            if (!await _seniorityRepository.Exists(request.Id))
            {
                throw SeniorityLevelNotFoundException.ForId(request.Id);
            }

            var seniority = await _seniorityRepository.GetById(request.Id);
            
            await _seniorityRepository.Delete(seniority);
            
            return Unit.Value;
        }
    }
}