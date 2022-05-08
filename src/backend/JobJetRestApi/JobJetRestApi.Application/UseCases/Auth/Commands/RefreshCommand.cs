using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Auth.Commands;

public class RefreshCommand : IRequest<(LoginResponse LoginResponse, string RefreshToken)>
{
    public string RefreshToken { get; private set; }
    public string IpAddress { get; private set; }

    public RefreshCommand(string refreshToken, string ipAddress)
    {
        RefreshToken = refreshToken;
        IpAddress = ipAddress;
    }
}