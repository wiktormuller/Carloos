namespace JobJetRestApi.Infrastructure.Options;

public class RefreshTokenOptions
{
    public const string RefreshToken = "RefreshToken";
    
    public string Issuer { get; set; }

    public string Secret { get; set; }

    public int ExpirationInHours { get; set; }
}