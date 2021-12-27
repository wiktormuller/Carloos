using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.TechnologyType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.CommandsHandlers
{
    public class UpdateTechnologyTypeCommandHandler : IRequestHandler<UpdateTechnologyTypeCommand>
    {
        private readonly ITechnologyTypeRepository _technologyTypeRepository;

        public UpdateTechnologyTypeCommandHandler(ITechnologyTypeRepository technologyTypeRepository)
        {
            _technologyTypeRepository = technologyTypeRepository;
        }

        public async Task<Unit> Handle(UpdateTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _technologyTypeRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            if (await _technologyTypeRepository.Exists(request.Name))
            {
                throw new ArgumentException(request.Name);
                // @TODO - Throw Domain Exception
            }

            var technologyType = await _technologyTypeRepository.GetById(request.Id);

            technologyType.UpdateName(request.Name);

            await _technologyTypeRepository.Update();

            return Unit.Value;
        }
    }
}