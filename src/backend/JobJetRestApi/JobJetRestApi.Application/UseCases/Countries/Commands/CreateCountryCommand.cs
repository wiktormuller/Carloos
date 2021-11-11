using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.Commands
{
    public class CreateCountryCommand : IRequest<int>
    {
        public CreateCountryCommand()
        {
            
        }
    }
}