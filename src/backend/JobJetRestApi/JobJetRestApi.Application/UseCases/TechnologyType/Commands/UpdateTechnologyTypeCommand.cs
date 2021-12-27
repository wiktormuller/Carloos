using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.Commands
{
    public class UpdateTechnologyTypeCommand : IRequest
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        
        public UpdateTechnologyTypeCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}