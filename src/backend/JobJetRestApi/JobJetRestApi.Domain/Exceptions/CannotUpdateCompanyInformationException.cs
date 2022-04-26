namespace JobJetRestApi.Domain.Exceptions;

public class CannotUpdateCompanyInformationException : System.Exception
{
    private CannotUpdateCompanyInformationException(string message) : base(message) {}

    public static CannotUpdateCompanyInformationException YouAreNotCompanyOwner(int companyId) =>
        new CannotUpdateCompanyInformationException($"Cannot update company information, because you are not owner of company with Id: #{companyId}.");
}