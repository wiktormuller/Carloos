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

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public ResetPasswordCommandHandler(UserManager<User> userManager, 
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return Unit.Value;
        }

        var resultOfResetting = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (resultOfResetting.Errors.Any())
        {
            throw InvalidEmailConfirmationTokenException.ForToken(request.Token);
        }
        
        return Unit.Value;
    }
}