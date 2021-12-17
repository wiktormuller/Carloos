namespace JobJetRestApi.Domain.Exceptions
{
    public class JobOfferNotFoundException : System.Exception
    {
        private JobOfferNotFoundException(string message) : base(message) {}

        public static JobOfferNotFoundException ForId(int id) =>
            new JobOfferNotFoundException($"Job offer with Id: #{id} not found.");
    }
}