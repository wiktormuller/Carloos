using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.Commands
{
    public class CreateTechnologyTypeCommand : IRequest<int>
    {
        public CreateTechnologyTypeCommand()
        {
            
        }
    }
}