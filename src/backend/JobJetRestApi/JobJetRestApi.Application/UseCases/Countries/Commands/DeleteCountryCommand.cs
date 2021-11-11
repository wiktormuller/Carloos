using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.Commands
{
    public class DeleteCountryCommand : IRequest
    {
        public int Id { get; private set; }
        
        public DeleteCountryCommand(int id)
        {
            Id = id;
        }
    }
}