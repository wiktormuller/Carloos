using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Users.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.CommandsHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.Exists(request.Id))
            {
                throw UserNotFoundException.ForId(request.Id);
            }

            var user = await _userRepository.GetById(request.Id);

            await _userRepository.Delete(user.Id);

            return Unit.Value;
        }
    }
}