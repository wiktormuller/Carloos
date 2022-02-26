using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; private set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}