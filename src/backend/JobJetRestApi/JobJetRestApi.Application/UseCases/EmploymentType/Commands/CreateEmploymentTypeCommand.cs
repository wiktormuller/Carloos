using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.Commands
{
    public class CreateEmploymentTypeCommand : IRequest<int>
    {
        public CreateEmploymentTypeCommand()
        {
            
        }
    }
}