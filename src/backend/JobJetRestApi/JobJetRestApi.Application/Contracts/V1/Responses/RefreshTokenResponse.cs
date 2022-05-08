using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class RefreshTokenResponse
{
    public string Token { get; private set; }
    
    public RefreshTokenResponse(RefreshToken refreshToken)
    {
        Token = refreshToken.Token;
    }
}