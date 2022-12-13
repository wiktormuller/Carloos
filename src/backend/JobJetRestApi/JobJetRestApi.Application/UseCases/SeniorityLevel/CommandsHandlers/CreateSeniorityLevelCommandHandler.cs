using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.CommandsHandlers
{
    public class CreateSeniorityLevelCommandHandler : IRequestHandler<CreateSeniorityLevelCommand, int>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        
        public CreateSeniorityLevelCommandHandler(ISeniorityRepository seniorityRepository)
        {
            _seniorityRepository = Guard.Against.Null(seniorityRepository, nameof(seniorityRepository));
        }
        
        /// <exception cref="SeniorityLevelAlreadyExistsException"></exception>
        public async Task<int> Handle(CreateSeniorityLevelCommand request, CancellationToken cancellationToken)
        {
            if (await _seniorityRepository.ExistsAsync(request.Name))
            {
                throw SeniorityLevelAlreadyExistsException.ForName(request.Name);
            }

            var seniorityLevel = new Seniority(request.Name);

            await _seniorityRepository.CreateAsync(seniorityLevel);

            return seniorityLevel.Id;
        }
    }
}