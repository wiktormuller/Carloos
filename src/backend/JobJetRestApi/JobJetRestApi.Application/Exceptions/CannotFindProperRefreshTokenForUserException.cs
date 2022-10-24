namespace JobJetRestApi.Application.Exceptions;

public class CannotFindProperRefreshTokenForUserException : System.Exception
{
    private CannotFindProperRefreshTokenForUserException(string message) : base(message) {}

    public static CannotFindProperRefreshTokenForUserException Default() =>
        new CannotFindProperRefreshTokenForUserException("Cannot find proper refresh token for user user.");
}