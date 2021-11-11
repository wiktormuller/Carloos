using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.TechnologyType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.CommandsHandlers
{
    public class DeleteTechnologyTypeCommandHandler : IRequestHandler<DeleteTechnologyTypeCommand>
    {
        public DeleteTechnologyTypeCommandHandler()
        {
            
        }
        
        public Task<Unit> Handle(DeleteTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}