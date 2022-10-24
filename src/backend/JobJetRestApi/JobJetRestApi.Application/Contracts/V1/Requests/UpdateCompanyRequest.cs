namespace JobJetRestApi.Application.Contracts.V1.Requests
{
    public class UpdateCompanyRequest
    {
        public string Description { get; private set; }
        public int NumberOfPeople { get; private set; }

        public UpdateCompanyRequest(string description, int numberOfPeople)
        {
            Description = description;
            NumberOfPeople = numberOfPeople;
        }
    }
}