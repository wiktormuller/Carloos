using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.Commands
{
    public class DeleteEmploymentTypeCommand : IRequest
    {
        public int Id { get; private set; }
        
        public DeleteEmploymentTypeCommand(int id)
        {
            Id = id;
        }
    }
}