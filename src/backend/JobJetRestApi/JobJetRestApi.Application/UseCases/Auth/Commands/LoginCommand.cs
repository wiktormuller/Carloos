using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Auth.Commands;

public class LoginCommand : IRequest<(LoginResponse LoginResponse, string RefreshToken)>
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string IpAddress { get; private set; }

    public LoginCommand(string email, string password, string ipAddress)
    {
        Email = email;
        Password = password;
        IpAddress = ipAddress;
    }
}