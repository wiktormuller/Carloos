using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.CommandsHandlers
{
    public class DeleteSeniorityLevelCommandHandler : IRequestHandler<DeleteSeniorityLevelCommand>
    {
        public DeleteSeniorityLevelCommandHandler()
        {
            
        }
        
        public Task<Unit> Handle(DeleteSeniorityLevelCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}