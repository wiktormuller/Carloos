using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.TechnologyType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.CommandsHandlers
{
    public class CreateTechnologyTypeCommandHandler : IRequestHandler<CreateTechnologyTypeCommand, int>
    {
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        
        public CreateTechnologyTypeCommandHandler(ITechnologyTypeRepository technologyTypeRepository)
        {
            _technologyTypeRepository = Guard.Against.Null(technologyTypeRepository, nameof(technologyTypeRepository));
        }

        /// <exception cref="TechnologyTypeAlreadyExistsException"></exception>
        public async Task<int> Handle(CreateTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _technologyTypeRepository.ExistsAsync(request.Name))
            {
                throw TechnologyTypeAlreadyExistsException.ForName(request.Name);
            }
            
            var technologyType = new Domain.Entities.TechnologyType(request.Name);

            await _technologyTypeRepository.CreateAsync(technologyType);

            return technologyType.Id;
        }
    }
}