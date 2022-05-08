namespace JobJetRestApi.Application.Exceptions;

public class RefreshTokenIsMissedInRequestException : System.Exception
{
    private RefreshTokenIsMissedInRequestException(string message) : base(message) {}

    public static RefreshTokenIsMissedInRequestException Default() =>
        new RefreshTokenIsMissedInRequestException("Refresh token is missed in request.");
}