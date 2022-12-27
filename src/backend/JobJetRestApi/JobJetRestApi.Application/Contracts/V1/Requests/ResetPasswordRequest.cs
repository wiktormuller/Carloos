namespace JobJetRestApi.Application.Contracts.V1.Requests;

public class ResetPasswordRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}