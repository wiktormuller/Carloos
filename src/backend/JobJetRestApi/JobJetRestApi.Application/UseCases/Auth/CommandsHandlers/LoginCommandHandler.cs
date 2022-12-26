using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Auth.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Application.UseCases.Auth.CommandsHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, (LoginResponse LoginResponse, string RefreshToken)>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public LoginCommandHandler(IUserRepository userRepository, 
        IJwtService jwtService, 
        UserManager<User> userManager, 
        SignInManager<User> signInManager)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<(LoginResponse LoginResponse, string RefreshToken)> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            throw AuthException.Default();
        }
        
        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed)
        {
            throw AuthException.EmailIsNotConfirmed();
        }

        // Validate User Identity => options.Lockout values
        var checkingPasswordSignIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        
        if (checkingPasswordSignIn.IsNotAllowed)
        {
            throw AuthException.Default();
        }
        
        if (checkingPasswordSignIn.IsLockedOut)
        {
            throw AuthException.AccountIsLockedOut();
        }

        var userRoles = await _userRepository.GetUserRolesAsync(user);
        var accessToken = _jwtService.GenerateAccessJwt(user, userRoles);
        var refreshToken = _jwtService.GenerateRefreshJwt(user, request.IpAddress);
        
        user.AddRefreshToken(refreshToken);
        await _userRepository.UpdateAsync(user);

        var loginResponse = new LoginResponse(user.Id, user.Email, accessToken);

        return (loginResponse, refreshToken.Token);
    }
}