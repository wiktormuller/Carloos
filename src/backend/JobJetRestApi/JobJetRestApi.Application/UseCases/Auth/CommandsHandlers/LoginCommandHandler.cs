using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Auth.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Auth.CommandsHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    
    public LoginCommandHandler(IUserRepository userRepository, 
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
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
        var token = _jwtService.GenerateJwt(user, userRoles);
        
        return new LoginResponse(user.Id, user.Email, token);
    }
}