using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.CommandsHandlers
{
    public class CreateSeniorityLevelCommandHandler : IRequestHandler<CreateSeniorityLevelCommand, int>
    {
        public CreateSeniorityLevelCommandHandler()
        {
            
        }

        public Task<int> Handle(CreateSeniorityLevelCommand request, CancellationToken cancellationToken)
        {
            var result = 123;

            return Task.FromResult(123);
        }
    }
}