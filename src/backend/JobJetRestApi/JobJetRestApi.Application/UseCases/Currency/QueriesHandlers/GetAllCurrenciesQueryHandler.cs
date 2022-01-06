using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.QueriesHandlers
{
    public class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, List<CurrencyResponse>>
    {
        private readonly ICurrencyRepository _currencyRepository;
        
        public GetAllCurrenciesQueryHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<List<CurrencyResponse>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var currencies = await _currencyRepository.GetAll();

            return currencies
                .Select(x => new CurrencyResponse(x.Id, x.Name, x.IsoCode))
                .ToList();
        }
    }
}