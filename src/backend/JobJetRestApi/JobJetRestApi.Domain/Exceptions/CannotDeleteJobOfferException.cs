namespace JobJetRestApi.Domain.Exceptions;

public class CannotDeleteJobOfferException : System.Exception
{
    private CannotDeleteJobOfferException(string message) : base(message) {}

    public static CannotDeleteJobOfferException YouAreNotJobOfferOwner(int jobOffer) =>
        new CannotDeleteJobOfferException($"Cannot delete job offer, because you are not owner of job offer with Id: #{jobOffer}.");
}