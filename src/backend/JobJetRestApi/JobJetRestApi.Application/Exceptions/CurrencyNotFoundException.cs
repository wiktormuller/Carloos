namespace JobJetRestApi.Application.Exceptions
{
    public class CurrencyNotFoundException : System.Exception
    {
        private CurrencyNotFoundException(string message) : base(message) {}

        public static CurrencyNotFoundException ForId(int id) =>
            new CurrencyNotFoundException($"Currency with Id: #{id} not found.");
    }
}