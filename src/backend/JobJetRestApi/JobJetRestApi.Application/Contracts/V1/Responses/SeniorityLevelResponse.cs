namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class SeniorityLevelResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public SeniorityLevelResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}