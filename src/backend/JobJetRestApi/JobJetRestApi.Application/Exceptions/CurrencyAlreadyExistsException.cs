namespace JobJetRestApi.Application.Exceptions
{
    public class CurrencyAlreadyExistsException : System.Exception
    {
        private CurrencyAlreadyExistsException(string message) : base(message) {}

        public static CurrencyAlreadyExistsException ForName(string name) =>
            new CurrencyAlreadyExistsException($"Currency with name: '{name}' already exists.");
        
        public static CurrencyAlreadyExistsException ForIsoCode(string isoCode) =>
            new CurrencyAlreadyExistsException($"Currency with iso code: '{isoCode}' not found.");
    }
}