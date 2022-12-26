using System.Collections.Generic;

namespace JobJetRestApi.Application.Exceptions;

public class AuthException : System.Exception
{
    private AuthException(string message) : base(message) {}

    public static AuthException Default() =>
        new AuthException($"Email address or password is incorrect.");

    public static AuthException EmailIsNotConfirmed() =>
        new AuthException($"Email address is not confirmed.");

    public static AuthException AccountIsLockedOut() =>
        new AuthException($"Account is locked out. Try login later.");

    public static AuthException PasswordIncorrect(List<string> descriptions) =>
        new AuthException($"Password must match those predicates: ${string.Join(" | ", descriptions)}");
}