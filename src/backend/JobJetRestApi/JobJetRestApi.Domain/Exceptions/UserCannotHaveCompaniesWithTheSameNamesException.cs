namespace JobJetRestApi.Domain.Exceptions;

public class UserCannotHaveCompaniesWithTheSameNamesException : System.Exception
{
    private UserCannotHaveCompaniesWithTheSameNamesException(string message) : base(message) {}

    public static UserCannotHaveCompaniesWithTheSameNamesException ForName(string name) =>
        new UserCannotHaveCompaniesWithTheSameNamesException("User cannot have companies with the same name: '{name}'.");
}