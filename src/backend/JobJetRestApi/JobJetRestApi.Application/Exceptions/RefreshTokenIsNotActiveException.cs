namespace JobJetRestApi.Application.Exceptions;

public class RefreshTokenIsNotActiveException : System.Exception
{
    private RefreshTokenIsNotActiveException(string message) : base(message) {}

    public static RefreshTokenIsNotActiveException Default() =>
        new RefreshTokenIsNotActiveException("Refresh token is no longer active.");
}