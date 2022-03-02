using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Repositories;
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
            _technologyTypeRepository = Guard.Against.Null(technologyTypeRepository, nameof(technologyTypeRepository));
        }

        /// <exception cref="TechnologyTypeNotFoundException"></exception>
        /// <exception cref="TechnologyTypeAlreadyExistsException"></exception>
        public async Task<Unit> Handle(UpdateTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            if (! await _technologyTypeRepository.ExistsAsync(request.Id))
            {
                throw TechnologyTypeNotFoundException.ForId(request.Id);
            }

            if (await _technologyTypeRepository.ExistsAsync(request.Name))
            {
                throw TechnologyTypeAlreadyExistsException.ForName(request.Name);
            }

            var technologyType = await _technologyTypeRepository.GetByIdAsync(request.Id);

            technologyType.UpdateName(request.Name);

            await _technologyTypeRepository.UpdateAsync();

            return Unit.Value;
        }
    }
}