using JetBrains.Annotations;

namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class RefreshTokenResponse
{
    public int Id { get; private set; }
    public bool IsExpired { get; private set; }
    public bool IsActive { get; private set; }
    [CanBeNull] public string ReplacedByToken { get; private set; }
    public string Token { get; private set; }
    
    public RefreshTokenResponse(int id, bool isExpired, bool isActive, string replacedByToken, string token)
    {
        Id = id;
        IsExpired = isExpired;
        IsActive = isActive;
        ReplacedByToken = replacedByToken;
        Token = token;
    }
}