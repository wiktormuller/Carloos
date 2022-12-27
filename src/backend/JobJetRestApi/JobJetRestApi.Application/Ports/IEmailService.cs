using System.Threading.Tasks;

namespace JobJetRestApi.Application.Ports;

public interface IEmailService
{
    Task SendAccountActivationEmailAsync(string recipientEmail, string userName, string emailConfirmationToken);
    Task SendRestPasswordEmailAsync(string recipientEmail, string passwordResettingToken);
}