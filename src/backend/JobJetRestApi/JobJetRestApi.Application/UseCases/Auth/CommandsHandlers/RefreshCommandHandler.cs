using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Auth.Commands;
using JobJetRestApi.Domain.Repositories;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace JobJetRestApi.Application.UseCases.Auth.CommandsHandlers;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, (LoginResponse LoginResponse, string RefreshToken)>
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    
    public RefreshCommandHandler(IUserRepository userRepository, 
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<(LoginResponse LoginResponse, string RefreshToken)> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        if (request.RefreshToken is null)
        {
            throw RefreshTokenIsMissedInRequestException.Default();
        }
        
        var claimsPrincipal = _jwtService.GetClaimsPrincipalFromRefreshToken(request.RefreshToken);
        
        var userId = int.Parse(claimsPrincipal.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);

        var user = await _userRepository.GetByIdAsync(userId);
        var userRoles = await _userRepository.GetUserRolesAsync(user);
        
        var refreshTokenOfUser = user.RefreshTokens
                .FirstOrDefault(refreshToken => refreshToken.Token == request.RefreshToken);

        if (refreshTokenOfUser is null)
        {
            throw CannotFindProperRefreshTokenForUserException.Default();
        }

        if (!refreshTokenOfUser.IsActive)
        {
            throw RefreshTokenIsNotActiveException.Default();
        }

        var newRefreshToken = _jwtService.GenerateRefreshJwt(user, request.IpAddress);
        var newAccessToken = _jwtService.GenerateAccessJwt(user, userRoles);
        
        refreshTokenOfUser.Revoke(user.Email, "someIpAddress", newRefreshToken.Token);
        user.AddRefreshToken(newRefreshToken);

        var loginResponse = new LoginResponse(user.Id, user.Email, newAccessToken);

        return (loginResponse, newRefreshToken.Token);
    }
}