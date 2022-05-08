using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Auth.Commands;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace JobJetRestApi.Application.UseCases.Auth.CommandsHandlers;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    
    public RevokeTokenCommandHandler(IJwtService jwtService, 
        IUserRepository userRepository)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
    }
    
    public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        if (request.RefreshToken is null)
        {
            throw RefreshTokenIsMissedInRequestException.Default();
        }
        
        var claimsPrincipal = _jwtService.GetClaimsPrincipalFromRefreshToken(request.RefreshToken);
        
        var userId = int.Parse(claimsPrincipal.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);

        var user = await _userRepository.GetByIdAsync(userId);
        
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
        
        refreshTokenOfUser.Revoke(user.Email, request.IpAddress);

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}