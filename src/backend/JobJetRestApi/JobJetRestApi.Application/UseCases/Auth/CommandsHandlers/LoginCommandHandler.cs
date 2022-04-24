using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Auth.Commands;
using JobJetRestApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Application.UseCases.Auth.CommandsHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    
    public LoginCommandHandler(IUserRepository userRepository, 
        UserManager<User> userManager, 
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            throw AuthException.Default();
        }

        var areCredentialsCorrect = await _userManager.CheckPasswordAsync(user, request.Password); // @TODO - Move to repo?
        if (!areCredentialsCorrect)
        {
            throw AuthException.Default();
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateJwt(user, userRoles);
        return new LoginResponse(user.Id, user.Email, token);

    }
}