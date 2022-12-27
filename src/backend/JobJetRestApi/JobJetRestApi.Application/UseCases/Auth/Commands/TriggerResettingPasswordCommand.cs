using MediatR;

namespace JobJetRestApi.Application.UseCases.Auth.Commands;

public class TriggerResettingPasswordCommand : IRequest
{
    public string Email { get; private set; }

    public TriggerResettingPasswordCommand(string email)
    {
        Email = email;
    }
}