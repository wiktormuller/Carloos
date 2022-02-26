using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.TechnologyType.Commands;
using MediatR;
using JobJetRestApi.Application.Exceptions;

namespace JobJetRestApi.Application.UseCases.TechnologyType.CommandsHandlers
{
    public class UpdateTechnologyTypeCommandHandler : IRequestHandler<UpdateTechnologyTypeCommand>
    {
        private readonly ITechnologyTypeRepository _technologyTypeRepository;

        public UpdateTechnologyTypeCommandHandler(ITechnologyTypeRepository technologyTypeRepository)
        {
            _technologyTypeRepository = technologyTypeRepository;
        }

        /// <exception cref="TechnologyTypeNotFoundException"></exception>
        /// <exception cref="TechnologyTypeAlreadyExistsException"></exception>
        public async Task<Unit> Handle(UpdateTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            if (! await _technologyTypeRepository.Exists(request.Id))
            {
                throw TechnologyTypeNotFoundException.ForId(request.Id);
            }

            if (await _technologyTypeRepository.Exists(request.Name))
            {
                throw TechnologyTypeAlreadyExistsException.ForName(request.Name);
            }

            var technologyType = await _technologyTypeRepository.GetById(request.Id);

            technologyType.UpdateName(request.Name);

            await _technologyTypeRepository.Update();

            return Unit.Value;
        }
    }
}