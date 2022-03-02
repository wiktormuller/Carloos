using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using MediatR;
using JobJetRestApi.Application.Exceptions;

namespace JobJetRestApi.Application.UseCases.Currency.QueriesHandlers
{
    public class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, CurrencyResponse>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public GetCurrencyByIdQueryHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = Guard.Against.Null(currencyRepository, nameof(currencyRepository));
        }

        public async Task<CurrencyResponse> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            if (! await _currencyRepository.ExistsAsync(request.Id))
            {
                throw CurrencyNotFoundException.ForId(request.Id);
            }

            var currency = await _currencyRepository.GetByIdAsync(request.Id);
            
            var result = new CurrencyResponse(currency.Id, currency.Name, currency.IsoCode);

            return result;
        }
    }
}