using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.Commands
{
    public class DeleteTechnologyTypeCommand : IRequest
    {
        public int Id { get; private set; }
        
        public DeleteTechnologyTypeCommand(int id)
        {
            Id = id;
        }
    }
}