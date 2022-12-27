using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Auth.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Application.UseCases.Auth.CommandsHandlers;

public class TriggerResettingPasswordCommandHandler : IRequestHandler<TriggerResettingPasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public TriggerResettingPasswordCommandHandler(UserManager<User> userManager, 
        IUserRepository userRepository, 
        IEmailService emailService)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(TriggerResettingPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return Unit.Value; // Don't throw exception, because we don't want to expose emails in our system
        }
        
        // Send email
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        await _emailService.SendRestPasswordEmailAsync(request.Email, token);
        
        return Unit.Value;
    }
}