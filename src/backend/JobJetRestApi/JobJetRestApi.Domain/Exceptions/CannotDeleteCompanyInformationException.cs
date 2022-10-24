namespace JobJetRestApi.Domain.Exceptions;

public class CannotDeleteCompanyInformationException : System.Exception
{
    private CannotDeleteCompanyInformationException(string message) : base(message) {}

    public static CannotDeleteCompanyInformationException YouAreNotCompanyOwner(int companyId) =>
        new CannotDeleteCompanyInformationException($"Cannot delete company, because you are not owner of company with Id: #{companyId}.");
}