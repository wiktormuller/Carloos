using MediatR;

namespace JobJetRestApi.Application.UseCases.Auth.Commands;

public class ActivateAccountCommand : IRequest
{
    public string Email { get; private set; }
    public string Token { get; private set; }
    
    public ActivateAccountCommand(string email, string token)
    {
        Email = email;
        Token = token;
    }
}