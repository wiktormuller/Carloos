using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Currency.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.CommandsHandlers
{
    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, int>
    {
        private readonly ICurrencyRepository _currencyRepository;
        
        public CreateCurrencyCommandHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = Guard.Against.Null(currencyRepository, nameof(currencyRepository));
        }
        
        /// <exception cref="CurrencyNotFoundException"></exception>
        public async Task<int> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (await _currencyRepository.Exists(request.IsoCode, request.IsoNumber))
            {
                throw CurrencyAlreadyExistsException.ForIsoCode(request.IsoCode);
            }

            var currency = new Domain.Entities.Currency(request.Name, request.IsoCode, request.IsoNumber);

            await _currencyRepository.Create(currency);

            return currency.Id;
        }
    }
}