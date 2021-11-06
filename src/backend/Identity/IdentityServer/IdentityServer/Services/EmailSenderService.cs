using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace IdentityServer.Services
{
    public class EmailSenderService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}