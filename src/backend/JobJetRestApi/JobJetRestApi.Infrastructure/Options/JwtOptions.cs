namespace JobJetRestApi.Infrastructure.Options;

public class JwtOptions
{
    public const string Jwt = "Jwt";
        
    public string Issuer { get; set; }

    public string Secret { get; set; }

    public int ExpirationInHours { get; set; }
}