using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.Commands
{
    public class UpdateCountryCommand : IRequest
    {
        public int Id { get; private set; }
        
        public UpdateCountryCommand(int id)
        {
            Id = id;
        }
    }
}