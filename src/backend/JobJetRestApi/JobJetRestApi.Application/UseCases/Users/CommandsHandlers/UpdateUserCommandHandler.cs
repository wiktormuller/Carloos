using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Users.Commands;
using JobJetRestApi.Domain.Repositories;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.CommandsHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.ExistsAsync(request.Id))
            {
                throw UserNotFoundException.ForId(request.Id);
            }

            var user = await _userRepository.GetByIdAsync(request.Id);

            user.UpdateUsername(request.UserName);

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}