using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Users.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Application.UseCases.Users.CommandsHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public CreateUserCommandHandler(IUserRepository userRepository, 
            IRoleRepository roleRepository, 
            IEmailService emailService, 
            UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _emailService = emailService;
            _userManager = userManager;
        }
        
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsAsync(request.Email))
            {
                throw UserAlreadyExistsException.ForEmail(request.Email);
            }

            if (await _userRepository.ExistsWithUserName(request.Name))
            {
                throw UserAlreadyExistsException.ForName(request.Name);
            }
            
            var user = new User(request.Email, request.Name);
            
            // Validate password
            var passwordErrors = new List<string>();

            var validators = _userManager.PasswordValidators;

            foreach(var validator in validators)
            {
                // Validate User Identity => options.Password values and also some custom validators
                var result = await validator.ValidateAsync(_userManager, user, request.Password);

                if (result.Succeeded)
                {
                    continue;
                }
                passwordErrors.AddRange(result.Errors.Select(error => error.Description));
            }

            if (passwordErrors.Any())
            {
                throw AuthException.PasswordIncorrect(passwordErrors);
            }

            var userRole = await _roleRepository.GetByNameAsync("User");

            await _userRepository.CreateAsync(user, request.Password);

            await _userRepository.AssignRoleToUser(user, userRole);

            // Send email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailService.SendAccountActivationEmailAsync(request.Email, request.Name, token);

            return user.Id;
        }
    }
}