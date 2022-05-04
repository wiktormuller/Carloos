using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.Commands
{
    public class CreateCountryCommand : IRequest<int>
    {
        public string Name { get; private set; }
        public string Alpha2Code { get; private set; }
        public string Alpha3Code { get; private set; }
        public int NumericCode { get; private set; }
        public decimal LatitudeOfCapital { get; private set; }
        public decimal LongitudeOfCapital { get; private set; }
        
        public CreateCountryCommand(string name, string alpha2Code, string alpha3Code, int numericCode,
            decimal latitudeOfCapital, decimal longitudeOfCapital)
        {
            Name = name;
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            NumericCode = numericCode;
            LatitudeOfCapital = latitudeOfCapital;
            LongitudeOfCapital = longitudeOfCapital;
        }
    }
}