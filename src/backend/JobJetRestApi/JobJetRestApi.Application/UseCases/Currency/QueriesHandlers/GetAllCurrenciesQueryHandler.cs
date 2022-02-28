using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Application.UseCases.Currency.QueriesHandlers
{
    public class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, List<CurrencyResponse>>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMemoryCache _memoryCache;
        
        public GetAllCurrenciesQueryHandler(ICurrencyRepository currencyRepository, 
            IMemoryCache memoryCache)
        {
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
            _currencyRepository = Guard.Against.Null(currencyRepository, nameof(currencyRepository));
        }

        public async Task<List<CurrencyResponse>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "currenciesKey";
            
            if (!_memoryCache.TryGetValue(cacheKey, out List<Domain.Entities.Currency> currencies))
            {
                currencies = await _currencyRepository.GetAll();
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(cacheKey, currencies, cacheExpiryOptions);
            }

            return currencies
                .Select(x => new CurrencyResponse(x.Id, x.Name, x.IsoCode))
                .ToList();
        }
    }
}