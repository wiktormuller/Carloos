namespace JobJetRestApi.Application.Exceptions
{
    public class CountryAlreadyExistsException : System.Exception
    {
        private CountryAlreadyExistsException(string message) : base(message) {}

        public static CountryAlreadyExistsException ForName(string name) =>
            new CountryAlreadyExistsException($"Country with name: '{name}' already exists.");
    }
}