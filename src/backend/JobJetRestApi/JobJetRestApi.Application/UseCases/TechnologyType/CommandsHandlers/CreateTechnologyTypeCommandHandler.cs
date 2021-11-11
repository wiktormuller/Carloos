using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.TechnologyType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.CommandsHandlers
{
    public class CreateTechnologyTypeCommandHandler : IRequestHandler<CreateTechnologyTypeCommand, int>
    {
        public CreateTechnologyTypeCommandHandler()
        {
            
        }

        public Task<int> Handle(CreateTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            var result = 123;

            return Task.FromResult(result);
        }
    }
}