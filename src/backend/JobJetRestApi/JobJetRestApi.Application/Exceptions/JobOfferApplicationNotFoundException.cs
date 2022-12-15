namespace JobJetRestApi.Application.Exceptions;

public class JobOfferApplicationNotFoundException : System.Exception
{
    private JobOfferApplicationNotFoundException(string message) : base(message) {}

    public static JobOfferApplicationNotFoundException ForId(int id) =>
        new JobOfferApplicationNotFoundException($"Job Offer Application with Id: #{id} not found.");
}