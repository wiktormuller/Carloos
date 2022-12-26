namespace JobJetRestApi.Application.Exceptions;

public class InvalidEmailConfirmationTokenException : System.Exception
{
    private InvalidEmailConfirmationTokenException(string message) : base(message) {}

    public static InvalidEmailConfirmationTokenException ForToken(string token) =>
        new InvalidEmailConfirmationTokenException($"Email confirmation token: '${token}' is invalid.");
}