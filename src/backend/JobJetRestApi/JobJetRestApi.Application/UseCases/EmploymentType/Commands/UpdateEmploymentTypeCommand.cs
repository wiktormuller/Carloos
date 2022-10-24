using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.Commands
{
    public class UpdateEmploymentTypeCommand : IRequest
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        
        public UpdateEmploymentTypeCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}