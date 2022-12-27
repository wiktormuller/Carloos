using MediatR;

namespace JobJetRestApi.Application.UseCases.Auth.Commands;

public class ResetPasswordCommand : IRequest
{    
    public string Email { get; private set; }
    public string Token { get; private set; }
    public string NewPassword { get; private set; }
    
    public ResetPasswordCommand(string email, string token, string newPassword)
    {
        Email = email;
        Token = token;
        NewPassword = newPassword;
    }
}