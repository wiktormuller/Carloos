namespace JobJetRestApi.Application.Exceptions
{
    public class TechnologyTypeAlreadyExistsException : System.Exception
    {
        private TechnologyTypeAlreadyExistsException(string message) : base(message) {}

        public static TechnologyTypeAlreadyExistsException ForName(string name) =>
            new TechnologyTypeAlreadyExistsException($"Technology type with name: '{name}' already exists.");
    }
}