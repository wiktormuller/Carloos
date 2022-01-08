using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.Commands
{
    public class DeleteCompanyCommand : IRequest
    {
        public int Id { get; private set; }
        
        public DeleteCompanyCommand(int id)
        {
            Id = id;
        }
    }
}