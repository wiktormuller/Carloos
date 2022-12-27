namespace JobJetRestApi.Application.Exceptions;

public class InvalidResetPasswordTokenException : System.Exception
{
    private InvalidResetPasswordTokenException(string message) : base(message) {}

    public static InvalidResetPasswordTokenException ForToken(string token) =>
        new InvalidResetPasswordTokenException($"Rest password token: '${token}' is invalid.");
}
