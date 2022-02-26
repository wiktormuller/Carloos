using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        
        public UpdateUserCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}