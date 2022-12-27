using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Auth.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Application.UseCases.Auth.CommandsHandlers;

public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public ActivateAccountCommandHandler(UserManager<User> userManager, 
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }
    
    public async Task<Unit> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return Unit.Value;
        }

        var resultOfConfirmation = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (resultOfConfirmation.Errors.Any())
        {
            throw InvalidEmailConfirmationTokenException.ForToken(request.Token);
        }
        
        return Unit.Value;
    }
}