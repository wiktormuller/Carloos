using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.Commands
{
    public class CreateCurrencyCommand : IRequest<int>
    {
        public string Name { get; private set; }
        public string IsoCode { get; private set; }
        public int IsoNumber { get; private set; }
        
        public CreateCurrencyCommand(string name, string isoCode, int isoNumber)
        {
            Name = name;
            IsoCode = isoCode;
            IsoNumber = isoNumber;
        }
    }
}