namespace JobJetRestApi.Domain.Exceptions
{
    public class CountryNotFoundException : System.Exception
    {
        private CountryNotFoundException(string message) : base(message) {}

        public static CountryNotFoundException ForId(int id) =>
            new CountryNotFoundException($"Country with Id: #{id} not found.");
    }
}