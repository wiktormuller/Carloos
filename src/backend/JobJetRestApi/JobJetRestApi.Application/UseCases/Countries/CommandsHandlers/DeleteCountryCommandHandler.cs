using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.Countries.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.CommandsHandlers
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        public DeleteCountryCommandHandler()
        {
            
        }

        public Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}