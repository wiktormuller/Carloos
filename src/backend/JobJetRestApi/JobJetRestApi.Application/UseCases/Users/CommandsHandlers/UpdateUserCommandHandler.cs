﻿using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Users.Commands;
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
            if (!await _userRepository.Exists(request.Id))
            {
                throw UserNotFoundException.ForId(request.Id);
            }

            var user = await _userRepository.GetById(request.Id);

            user.UpdateName(request.Name);

            await _userRepository.Update(user);

            return Unit.Value;
        }
    }
}