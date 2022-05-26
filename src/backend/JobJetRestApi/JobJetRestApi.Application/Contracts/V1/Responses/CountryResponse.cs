namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class CountryResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Alpha2Code { get; private set; }
        public decimal LatitudeOfCapital { get; private set; }
        public decimal LongitudeOfCapital { get; private set; }

        public CountryResponse(int id, string name, string alpha2Code, decimal latitudeOfCapital, decimal longitudeOfCapital)
        {
            Id = id;
            Name = name;
            Alpha2Code = alpha2Code;
            LatitudeOfCapital = latitudeOfCapital;
            LongitudeOfCapital = longitudeOfCapital;
        }
    }
}