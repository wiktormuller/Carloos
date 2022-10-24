using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }

        public CreateUserCommand(string email, string name, string password)
        {
            Email = email;
            Name = name;
            Password = password;
        }
    }
}