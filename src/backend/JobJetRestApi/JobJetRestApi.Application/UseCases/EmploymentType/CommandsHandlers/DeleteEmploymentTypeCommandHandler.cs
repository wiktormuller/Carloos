using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.EmploymentType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.CommandsHandlers
{
    public class DeleteEmploymentTypeCommandHandler : IRequestHandler<DeleteEmploymentTypeCommand>
    {
        public DeleteEmploymentTypeCommandHandler()
        {
            
        }

        public Task<Unit> Handle(DeleteEmploymentTypeCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}