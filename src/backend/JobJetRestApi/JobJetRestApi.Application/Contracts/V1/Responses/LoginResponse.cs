namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class LoginResponse
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string AccessToken { get; private set; }

    public LoginResponse(int id, string email, string accessToken)
    {
        Id = id;
        Email = email;
        AccessToken = accessToken;
    }
}