using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.CommandsHandlers
{
    public class DeleteSeniorityLevelCommandHandler : IRequestHandler<DeleteSeniorityLevelCommand>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        
        public DeleteSeniorityLevelCommandHandler(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = seniorityRepository;
        }
        
        public async Task<Unit> Handle(DeleteSeniorityLevelCommand request, CancellationToken cancellationToken)
        {
            if (!await _seniorityRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            var seniority = await _seniorityRepository.GetById(request.Id);
            
            await _seniorityRepository.Delete(seniority);
            
            return Unit.Value;
        }
    }
}