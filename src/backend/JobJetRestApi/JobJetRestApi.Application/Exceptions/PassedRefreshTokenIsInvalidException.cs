namespace JobJetRestApi.Application.Exceptions;

public class PassedRefreshTokenIsInvalidException : System.Exception
{
    private PassedRefreshTokenIsInvalidException(string message) : base(message) {}

    public static PassedRefreshTokenIsInvalidException Default() =>
        new PassedRefreshTokenIsInvalidException("Passed refresh token is invalid");
}