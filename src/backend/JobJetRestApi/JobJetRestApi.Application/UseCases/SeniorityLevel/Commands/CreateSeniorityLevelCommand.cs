using MediatR;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.Commands
{
    public class CreateSeniorityLevelCommand : IRequest<int>
    {
        public CreateSeniorityLevelCommand()
        {
            
        }
    }
}