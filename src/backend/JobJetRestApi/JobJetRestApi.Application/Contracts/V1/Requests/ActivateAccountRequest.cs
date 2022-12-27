namespace JobJetRestApi.Application.Contracts.V1.Requests;

public class ActivateAccountRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
}