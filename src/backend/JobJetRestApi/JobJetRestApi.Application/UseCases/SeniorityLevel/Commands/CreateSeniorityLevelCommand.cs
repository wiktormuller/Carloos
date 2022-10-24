using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.Commands
{
    public class CreateSeniorityLevelCommand : IRequest<int>
    {
        public string Name { get; private set; }
        
        public CreateSeniorityLevelCommand(string name)
        {
            Name = name;
        }
    }
}