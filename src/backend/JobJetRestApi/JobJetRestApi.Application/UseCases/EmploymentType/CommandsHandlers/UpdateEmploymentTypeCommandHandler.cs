using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.EmploymentType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.CommandsHandlers
{
    public class UpdateEmploymentTypeCommandHandler : IRequestHandler<UpdateEmploymentTypeCommand>
    {
        public UpdateEmploymentTypeCommandHandler()
        {
            
        }
        
        public Task<Unit> Handle(UpdateEmploymentTypeCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}