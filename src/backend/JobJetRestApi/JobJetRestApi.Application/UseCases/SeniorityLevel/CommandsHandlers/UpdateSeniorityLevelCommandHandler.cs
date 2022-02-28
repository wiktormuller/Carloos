using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using MediatR;
using JobJetRestApi.Application.Exceptions;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.CommandsHandlers
{
    public class UpdateSeniorityLevelCommandHandler : IRequestHandler<UpdateSeniorityLevelCommand>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        
        public UpdateSeniorityLevelCommandHandler(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = Guard.Against.Null(seniorityRepository, nameof(seniorityRepository));
        }

        /// <exception cref="SeniorityLevelNotFoundException"></exception>
        /// <exception cref="SeniorityLevelAlreadyExistsException"></exception>
        public async Task<Unit> Handle(UpdateSeniorityLevelCommand request, CancellationToken cancellationToken)
        {
            if (! await _seniorityRepository.Exists(request.Id))
            {
                throw SeniorityLevelNotFoundException.ForId(request.Id);
            }

            if (await _seniorityRepository.Exists(request.Name))
            {
                throw SeniorityLevelAlreadyExistsException.ForName(request.Name);
            }

            var seniority = await _seniorityRepository.GetById(request.Id);
            seniority.UpdateName(request.Name);

            await _seniorityRepository.Update();
            
            return Unit.Value;
        }
    }
}