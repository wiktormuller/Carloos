namespace JobJetRestApi.Application.Exceptions
{
    public class SeniorityLevelAlreadyExistsException : System.Exception
    {
        private SeniorityLevelAlreadyExistsException (string message) : base(message) {}

        public static SeniorityLevelAlreadyExistsException ForName(string name) =>
            new SeniorityLevelAlreadyExistsException($"Seniority level with name: '{name} already exists.'");
    }
}