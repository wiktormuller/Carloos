using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        
        public UpdateUserCommand(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }
}