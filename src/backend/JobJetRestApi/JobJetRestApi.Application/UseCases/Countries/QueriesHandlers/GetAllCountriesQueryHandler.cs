using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Application.UseCases.Countries.QueriesHandlers
{
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, List<CountryResponse>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMemoryCache _memoryCache;
        
        public GetAllCountriesQueryHandler(ICountryRepository countryRepository,
            IMemoryCache memoryCache)
        {
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
            _countryRepository = Guard.Against.Null(countryRepository, nameof(countryRepository));
        }
        
        public async Task<List<CountryResponse>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "countriesKey";
            
            if (!_memoryCache.TryGetValue(cacheKey, out List<Domain.Entities.Country> countries))
            {
                countries = await _countryRepository.GetAllAsync();
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(cacheKey, countries, cacheExpiryOptions);
            }

            return countries
                .Select(x => new CountryResponse(x.Id, x.Name, x.Alpha2Code))
                .ToList();
        }
    }
}