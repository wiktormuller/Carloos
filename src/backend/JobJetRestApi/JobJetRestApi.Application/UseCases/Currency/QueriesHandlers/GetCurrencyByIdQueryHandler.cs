using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.QueriesHandlers
{
    public class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, CurrencyResponse>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public GetCurrencyByIdQueryHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<CurrencyResponse> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            if (! await _currencyRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            var currency = await _currencyRepository.GetById(request.Id);
            
            var result = new CurrencyResponse(currency.Id, currency.Name, currency.IsoCode);

            return result;
        }
    }
}