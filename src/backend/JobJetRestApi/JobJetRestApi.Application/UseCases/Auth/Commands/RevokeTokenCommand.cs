using MediatR;

namespace JobJetRestApi.Application.UseCases.Auth.Commands;

public class RevokeTokenCommand : IRequest
{
    public string RefreshToken { get; private set; }
    public string IpAddress { get; private set; }

    public RevokeTokenCommand(string refreshToken, string ipAddress)
    {
        RefreshToken = refreshToken;
        IpAddress = ipAddress;
    }
}