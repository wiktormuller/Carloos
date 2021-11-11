using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.EmploymentType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.CommandsHandlers
{
    public class CreateEmploymentTypeCommandHandler : IRequestHandler<CreateEmploymentTypeCommand, int>
    {
        public CreateEmploymentTypeCommandHandler()
        {
            
        }

        public Task<int> Handle(CreateEmploymentTypeCommand request, CancellationToken cancellationToken)
        {
            var result = 123;

            return Task.FromResult(result);
        }
    }
}