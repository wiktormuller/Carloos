namespace JobJetRestApi.Application.Exceptions
{
    public class EmploymentTypeNotFoundException : System.Exception
    {
        private EmploymentTypeNotFoundException(string message) : base(message) {}

        public static EmploymentTypeNotFoundException ForId(int id) =>
            new EmploymentTypeNotFoundException($"Employment type with Id: #{id} not found");

        public static EmploymentTypeNotFoundException ForName(string name) =>
            new EmploymentTypeNotFoundException($"Employment type with name: '{name}' not found.");
    }
}