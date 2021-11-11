using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.TechnologyType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.CommandsHandlers
{
    public class UpdateTechnologyTypeCommandHandler : IRequestHandler<UpdateTechnologyTypeCommand>
    {
        public Task<Unit> Handle(UpdateTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}