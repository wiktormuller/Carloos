namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class TechnologyTypeResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public TechnologyTypeResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}