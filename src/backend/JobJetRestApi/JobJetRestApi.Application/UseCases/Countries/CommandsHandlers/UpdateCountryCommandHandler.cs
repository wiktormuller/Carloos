using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.Countries.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.CommandsHandlers
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand>
    {
        public UpdateCountryCommandHandler()
        {
            
        }

        public Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}