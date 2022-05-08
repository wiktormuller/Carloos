using System;

namespace JobJetRestApi.Domain.Entities;

public class RefreshToken
{
    public int Id { get; private set; }
    
    public string Token { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public DateTime UpdatedAt { get; private set; }
    
    public DateTime ExpiresAt { get; private set; }
    
    public DateTime? RevokedAt { get; private set; }
    
    public string RevokedByEmail { get; private set; }
    
    public string RevokedByIp { get; private set; }

    public string CreatedByEmail { get; private set; }
    
    public string CreatedByIp { get; private set; }
    
    public string ReplacedByToken { get; private set; }
    
    
    public bool IsActive => RevokedAt == null && !IsExpired;
    
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    
    public int UserId { get; private set; }

    private RefreshToken()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public RefreshToken(string token, string createdByEmail, string createdByIp, DateTime expiresAt) : this()
    {
        Token = token;
        CreatedByEmail = createdByEmail;
        CreatedByIp = createdByIp;
        ExpiresAt = expiresAt;
    }

    public void Revoke(string revokedByEmail, string revokedByIp, string newToken = null)
    {
        RevokedAt = DateTime.UtcNow;
        RevokedByEmail = revokedByEmail;
        RevokedByIp = revokedByIp;
        UpdatedAt = DateTime.UtcNow;
        ReplacedByToken = newToken;
    }
}