using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.CommandsHandlers
{
    public class UpdateSeniorityLevelCommandHandler : IRequestHandler<UpdateSeniorityLevelCommand>
    {
        public UpdateSeniorityLevelCommandHandler()
        {
            
        }

        public Task<Unit> Handle(UpdateSeniorityLevelCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}