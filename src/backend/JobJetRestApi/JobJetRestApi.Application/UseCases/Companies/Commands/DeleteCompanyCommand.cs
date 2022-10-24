using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.Commands
{
    public class DeleteCompanyCommand : IRequest
    {
        public int UserId { get; private set; }
        public int Id { get; private set; }
        
        public DeleteCompanyCommand(int userId, int id)
        {
            UserId = userId;
            Id = id;
        }
    }
}