namespace JobJetRestApi.Domain.Exceptions;

public class CompanyCannotHaveJobOffersWithTheSameNamesException : System.Exception
{
    private CompanyCannotHaveJobOffersWithTheSameNamesException(string message) : base(message) {}

    public static CompanyCannotHaveJobOffersWithTheSameNamesException ForName(string name) =>
        new CompanyCannotHaveJobOffersWithTheSameNamesException("Company cannot own job offers with the same name: '{name}'.");
}