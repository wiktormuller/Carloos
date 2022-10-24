using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Auth.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Auth.CommandsHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, (LoginResponse LoginResponse, string RefreshToken)>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    
    public LoginCommandHandler(IUserRepository userRepository, 
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<(LoginResponse LoginResponse, string RefreshToken)> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            throw AuthException.Default();
        }

        var areCredentialsCorrect = await _userRepository.CheckPasswordAsync(user, request.Password);
        if (!areCredentialsCorrect)
        {
            throw AuthException.Default();
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