using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.Commands
{
    public class UpdateEmploymentTypeCommand : IRequest
    {
        public int Id { get; private set; }
        
        public UpdateEmploymentTypeCommand(int id)
        {
            Id = id;
        }
    }
}