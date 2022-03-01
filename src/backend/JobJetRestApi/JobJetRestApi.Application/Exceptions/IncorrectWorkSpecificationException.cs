namespace JobJetRestApi.Application.Exceptions
{
    public class IncorrectWorkSpecificationException : System.Exception
    {
        private IncorrectWorkSpecificationException(string message) : base(message) {}

        public static IncorrectWorkSpecificationException ForName(string name) =>
            new IncorrectWorkSpecificationException($"Incorrect work specification for name: '{name}.");
    }
}