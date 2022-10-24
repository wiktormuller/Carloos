namespace JobJetRestApi.Application.Exceptions;

public class AuthException : System.Exception
{
    private AuthException(string message) : base(message) {}

    public static AuthException Default() =>
        new AuthException($"Email address or password is incorrect.");
}