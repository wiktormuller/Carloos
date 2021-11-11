using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.Commands
{
    public class DeleteSeniorityLevelCommand : IRequest
    {
        public int Id { get; private set; }
        
        public DeleteSeniorityLevelCommand(int id)
        {
            Id = id;
        }
    }
}