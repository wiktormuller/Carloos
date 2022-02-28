using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
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
            if (await _technologyTypeRepository.Exists(request.Name))
            {
                throw TechnologyTypeAlreadyExistsException.ForName(request.Name);
            }
            
            var technologyType = new Domain.Entities.TechnologyType(request.Name);

            var technologyTypeId = await _technologyTypeRepository.Create(technologyType);

            return technologyTypeId;
        }
    }
}