namespace JobJetRestApi.Application.Contracts.V1.Responses;

public class LoginResponse
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string Token { get; private set; }

    public LoginResponse(int id, string email, string token)
    {
        Id = id;
        Email = email;
        Token = token;
    }
}