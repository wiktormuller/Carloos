namespace JobJetRestApi.Application.Exceptions
{
    public class EmploymentTypeAlreadyExistsException : System.Exception
    {
        private EmploymentTypeAlreadyExistsException(string message) : base(message) {}

        public static EmploymentTypeAlreadyExistsException ForName(string name) =>
            new EmploymentTypeAlreadyExistsException($"Employment type with name: '{name}' already exists.");
    }
}