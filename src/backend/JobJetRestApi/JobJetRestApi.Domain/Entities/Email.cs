using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace JobJetRestApi.Domain.Entities;

public class Email
{
    public string ToEmail { get; }
    
    public string Subject { get; }
    
    public string Body { get; }
    
    public List<IFormFile> Attachments { get; }

    public Email(string toEmail, string subject, string body, List<IFormFile> attachments = null)
    {
        ToEmail = toEmail;
        Subject = subject;
        Body = body;
        Attachments = attachments;
    }
}