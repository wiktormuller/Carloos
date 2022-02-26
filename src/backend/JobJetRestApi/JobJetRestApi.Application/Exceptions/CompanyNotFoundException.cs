namespace JobJetRestApi.Application.Exceptions
{
    public class CompanyNotFoundException : System.Exception
    {
        private CompanyNotFoundException(string message) : base(message) {}

        public static CompanyNotFoundException ForName(string name) =>
            new CompanyNotFoundException($"Company with name: '{name} not found.'");

        public static CompanyNotFoundException ForId(int id) =>
            new CompanyNotFoundException($"Company with Id: #{id} not found.");
    }
}