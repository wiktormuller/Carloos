using System.Collections.Generic;

namespace JobJetRestApi.Application.Exceptions
{
    public class TechnologyTypeNotFoundException : System.Exception
    {
        private TechnologyTypeNotFoundException(string message) : base(message) {}

        public static TechnologyTypeNotFoundException ForId(int id) =>
            new TechnologyTypeNotFoundException($"Technology type with Id: #{id} not found.");
        
        public static TechnologyTypeNotFoundException ForIds(IEnumerable<int> ids) =>
            new TechnologyTypeNotFoundException($"Technology types with Ids: #{string.Join(", #", ids)} was not found.");

        public static TechnologyTypeNotFoundException ForName(string name) =>
            new TechnologyTypeNotFoundException($"Technology type with name: '{name}' not found.");
    }
}