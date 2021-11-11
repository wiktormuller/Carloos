using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.Commands
{
    public class UpdateTechnologyTypeCommand : IRequest
    {
        public int Id { get; private set; }
        
        public UpdateTechnologyTypeCommand(int id)
        {
            Id = id;
        }
    }
}