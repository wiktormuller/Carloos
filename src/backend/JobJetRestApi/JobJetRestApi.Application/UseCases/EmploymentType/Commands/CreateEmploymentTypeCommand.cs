using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.Commands
{
    public class CreateEmploymentTypeCommand : IRequest<int>
    {
        public string Name { get; private set; }
        
        public CreateEmploymentTypeCommand(string name)
        {
            Name = name;
        }
    }
}