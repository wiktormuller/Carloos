using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using JobJetRestApi.Domain.Entities;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.CommandsHandlers
{
    public class CreateSeniorityLevelCommandHandler : IRequestHandler<CreateSeniorityLevelCommand, int>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        
        public CreateSeniorityLevelCommandHandler(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = seniorityRepository;
        }

        public async Task<int> Handle(CreateSeniorityLevelCommand request, CancellationToken cancellationToken)
        {
            if (await _seniorityRepository.Exists(request.Name))
            {
                throw new ArgumentException(nameof(request.Name));
                // @TODO - Throw Domain Exception
            }

            var seniorityLevel = new Seniority(request.Name);

            await _seniorityRepository.Create(seniorityLevel);

            return seniorityLevel.Id;
        }
    }
}