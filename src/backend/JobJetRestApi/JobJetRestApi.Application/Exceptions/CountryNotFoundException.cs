namespace JobJetRestApi.Application.Exceptions
{
    public class CountryNotFoundException : System.Exception
    {
        private CountryNotFoundException(string message) : base(message) {}

        public static CountryNotFoundException ForId(int id) =>
            new CountryNotFoundException($"Country with Id: #{id} not found.");

        public static CountryNotFoundException ForName(string name) =>
            new CountryNotFoundException($"Country with name: '{name}' not found.");
    }
}