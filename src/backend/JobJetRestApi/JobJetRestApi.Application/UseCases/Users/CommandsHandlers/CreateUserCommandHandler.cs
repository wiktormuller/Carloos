using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Users.Commands;
using JobJetRestApi.Domain.Entities;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.CommandsHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        
        public CreateUserCommandHandler(IUserRepository userRepository, 
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsAsync(request.Email))
            {
                throw UserAlreadyExistsException.ForEmail(request.Email);
            }

            var user = new User(request.Email, request.Name);

            var userRole = await _roleRepository.GetByNameAsync("User");

            await _userRepository.CreateAsync(user, request.Password);
            
            await _userRepository.AssignRoleToUser(user, userRole);

            return user.Id;
        }
    }
}