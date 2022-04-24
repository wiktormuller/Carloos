namespace JobJetRestApi.Application.Contracts.V1.Requests;

public class LoginRequest //@TODO - Add Validator
{
    public string Email { get; set; }
    public string Password { get; set; }
}