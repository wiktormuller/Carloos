using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.Commands
{
    public class UpdateSeniorityLevelCommand : IRequest
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        
        public UpdateSeniorityLevelCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}