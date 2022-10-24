using System.Collections.Generic;
using System.Security.Claims;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Ports;

public interface IJwtService
{ 
    string GenerateAccessJwt(User user, IList<string> roles);
    RefreshToken GenerateRefreshJwt(User user, string ipAddress);
    ClaimsPrincipal GetClaimsPrincipalFromRefreshToken(string refreshToken);
}