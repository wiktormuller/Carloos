namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class EmploymentTypeResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public EmploymentTypeResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}