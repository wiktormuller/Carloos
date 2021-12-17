namespace JobJetRestApi.Domain.Exceptions
{
    public class TechnologyTypeNotFoundException : System.Exception
    {
        private TechnologyTypeNotFoundException(string message) : base(message) {}

        public static TechnologyTypeNotFoundException ForId(int id) =>
            new TechnologyTypeNotFoundException($"Technology type with Id: #{id} not found.");
    }
}