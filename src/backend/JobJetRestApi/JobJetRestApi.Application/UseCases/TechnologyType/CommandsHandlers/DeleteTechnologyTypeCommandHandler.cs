using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.TechnologyType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.CommandsHandlers
{
    public class DeleteTechnologyTypeCommandHandler : IRequestHandler<DeleteTechnologyTypeCommand>
    {
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        
        public DeleteTechnologyTypeCommandHandler(ITechnologyTypeRepository technologyTypeRepository)
        {
            _technologyTypeRepository = Guard.Against.Null(technologyTypeRepository, nameof(technologyTypeRepository));
        }
        
        public async Task<Unit> Handle(DeleteTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            if (! await _technologyTypeRepository.Exists(request.Id))
            {
                throw TechnologyTypeNotFoundException.ForId(request.Id);
            }

            var technologyType = await _technologyTypeRepository.GetById(request.Id);

            await _technologyTypeRepository.Delete(technologyType);
            
            return Unit.Value;
        }
    }
}