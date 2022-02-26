namespace JobJetRestApi.Application.Exceptions
{
    public class CompanyAlreadyExistsException : System.Exception
    {
        private CompanyAlreadyExistsException(string message) : base(message) {}

        public static CompanyAlreadyExistsException ForName(string name) =>
            new CompanyAlreadyExistsException($"Company with name: '{name} already exists.'");
    }
}