namespace JobJetRestApi.Application.Exceptions
{
    public class SeniorityLevelNotFoundException : System.Exception
    {
        private SeniorityLevelNotFoundException(string message) : base(message) {}

        public static SeniorityLevelNotFoundException ForId(int id) =>
            new SeniorityLevelNotFoundException($"Seniority level with Id: #{id} not found.");

        public static SeniorityLevelNotFoundException ForName(string name) =>
            new SeniorityLevelNotFoundException($"Seniority level with name: '{name}' not found.");
    }
}