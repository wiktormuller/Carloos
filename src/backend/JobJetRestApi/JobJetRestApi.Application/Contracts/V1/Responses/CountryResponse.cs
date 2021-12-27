namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class CountryResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Alpha2Code { get; private set; }

        public CountryResponse(int id, string name, string alpha2Code)
        {
            Id = id;
            Name = name;
            Alpha2Code = alpha2Code;
        }
    }
}