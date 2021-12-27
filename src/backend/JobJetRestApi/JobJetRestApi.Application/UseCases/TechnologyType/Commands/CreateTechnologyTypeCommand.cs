using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.Commands
{
    public class CreateTechnologyTypeCommand : IRequest<int>
    {
        public string Name { get; private set; }
        
        public CreateTechnologyTypeCommand(string name)
        {
            Name = name;
        }
    }
}