using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Users.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.CommandsHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailService _emailService;
        
        public CreateUserCommandHandler(IUserRepository userRepository, 
            IRoleRepository roleRepository, 
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _emailService = emailService;
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
            
            // Send email
            var email = new Email(request.Email, "Verify JobJet Account", "Welcome to JobJet!");
            await _emailService.SendAccountActivationEmailAsync(request.Email, request.Name);

            return user.Id;
        }
    }
}