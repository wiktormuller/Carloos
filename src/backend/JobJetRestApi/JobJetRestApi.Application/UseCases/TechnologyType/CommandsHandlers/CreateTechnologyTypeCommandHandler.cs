using System;
using System.Threading;
using System.Threading.Tasks;
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
            _technologyTypeRepository = technologyTypeRepository;
        }

        public async Task<int> Handle(CreateTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _technologyTypeRepository.Exists(request.Name))
            {
                throw new ArgumentException(nameof(request.Name));
                // @TODO - Throw Domain Exception
            }
            
            var technologyType = new Domain.Entities.TechnologyType(request.Name);

            var technologyTypeId = await _technologyTypeRepository.Create(technologyType);

            return technologyTypeId;
        }
    }
}