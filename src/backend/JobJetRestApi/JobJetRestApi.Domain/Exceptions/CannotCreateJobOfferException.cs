namespace JobJetRestApi.Domain.Exceptions;

public class CannotCreateJobOfferException : System.Exception
{
    private CannotCreateJobOfferException(string message) : base(message) {}

    public static CannotCreateJobOfferException YouAreNotCompanyOwner(int companyId) =>
        new CannotCreateJobOfferException($"Cannot create job offer, because you are not owner of company with Id: #{companyId}.");
}