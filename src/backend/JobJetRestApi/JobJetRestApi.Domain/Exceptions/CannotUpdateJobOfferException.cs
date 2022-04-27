namespace JobJetRestApi.Domain.Exceptions;

public class CannotUpdateJobOfferException : System.Exception
{
    private CannotUpdateJobOfferException(string message) : base(message) {}

    public static CannotUpdateJobOfferException YouAreNotJobOfferOwner(int jobOfferId) =>
        new CannotUpdateJobOfferException($"Cannot update job offer, because you are not owner of job offer with Id: #{jobOfferId}.");

}