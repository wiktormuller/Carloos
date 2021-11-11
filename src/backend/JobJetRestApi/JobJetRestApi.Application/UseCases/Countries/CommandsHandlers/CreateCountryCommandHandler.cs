using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.Countries.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.CommandsHandlers
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
    {
        public CreateCountryCommandHandler()
        {
            
        }

        public Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var countryId = 123;

            return Task.FromResult(countryId);
        }
    }
}