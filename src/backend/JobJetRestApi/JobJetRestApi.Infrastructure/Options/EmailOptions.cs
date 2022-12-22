namespace JobJetRestApi.Infrastructure.Options;

public class EmailOptions
{
    public const string EmailSmtpOptions = "EmailSMTP";
    
    public string EmailFrom { get; set; }
    
    public string DisplayName { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string Host { get; set; }
    
    public int Port { get; set; }
}