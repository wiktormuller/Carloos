namespace JobJetRestApi.Domain.Exceptions
{
    public class InvalidAddressException : System.Exception
    {
        private InvalidAddressException(string message) : base(message) {}

        public static InvalidAddressException Default(string address) =>
            new InvalidAddressException($"Address: '{address}' is not recognized as a real point on map.");
    }
}