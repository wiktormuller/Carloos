using System.IO;
using System.Threading.Tasks;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace JobJetRestApi.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailOptions _emailOptions;

    public EmailService(IOptionsSnapshot<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }
    
    public async Task SendAccountActivationEmailAsync(string recipientEmail, string userName)
    {
        // Email template
        var filePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\ActivateAccount\\Activate.html";
        var streamReader = new StreamReader(filePath);
        var mailText = await streamReader.ReadToEndAsync();
        streamReader.Close();

        mailText = mailText.Replace("[userName]", userName);

        var email = new Email(recipientEmail, "Activate your account at JobJet.", mailText);
        
        var mimeMessage = new MimeMessage();
        mimeMessage.Sender = MailboxAddress.Parse(_emailOptions.EmailFrom);
        mimeMessage.To.Add(MailboxAddress.Parse(email.ToEmail));
        mimeMessage.Subject = email.Subject;
        
        var builder = new BodyBuilder();
        if (email.Attachments != null)
        {
            foreach (var file in email.Attachments)
            {
                if (file.Length <= 0) continue;
                
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }
                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            }
        }
        
        builder.HtmlBody = email.Body;
        mimeMessage.Body = builder.ToMessageBody();
        
        using var smtp = new SmtpClient();
        
        await smtp.ConnectAsync(_emailOptions.Host, _emailOptions.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailOptions.Username, _emailOptions.Password);
        
        await smtp.SendAsync(mimeMessage);
        await smtp.DisconnectAsync(true);
    }
}