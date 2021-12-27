using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.CommandsHandlers
{
    public class UpdateSeniorityLevelCommandHandler : IRequestHandler<UpdateSeniorityLevelCommand>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        
        public UpdateSeniorityLevelCommandHandler(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = seniorityRepository;
        }

        public async Task<Unit> Handle(UpdateSeniorityLevelCommand request, CancellationToken cancellationToken)
        {
            if (! await _seniorityRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            if (await _seniorityRepository.Exists(request.Name))
            {
                throw new ArgumentException(nameof(request.Name));
                // @TODO = Throw Domain Exception
            }

            var seniority = await _seniorityRepository.GetById(request.Id);
            seniority.UpdateName(request.Name);

            await _seniorityRepository.Update();
            
            return Unit.Value;
        }
    }
}