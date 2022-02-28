using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Application.UseCases.TechnologyType.QueriesHandlers
{
    public class GetAllTechnologyTypesQueryHandler : IRequestHandler<GetAllTechnologyTypesQuery, List<TechnologyTypeResponse>>
    {
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        private readonly IMemoryCache _memoryCache;
        
        public GetAllTechnologyTypesQueryHandler(ITechnologyTypeRepository technologyTypeRepository,
            IMemoryCache memoryCache)
        {
            _technologyTypeRepository = Guard.Against.Null(technologyTypeRepository, nameof(technologyTypeRepository));
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
        }

        public async Task<List<TechnologyTypeResponse>> Handle(GetAllTechnologyTypesQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "technologyTypesKey";

            if (!_memoryCache.TryGetValue(cacheKey, out List<Domain.Entities.TechnologyType> technologyTypes))
            {
                technologyTypes = await _technologyTypeRepository.GetAll();
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                
                _memoryCache.Set(cacheKey, technologyTypes, cacheExpiryOptions);
            }

            return technologyTypes
                .Select(x => new TechnologyTypeResponse(x.Id, x.Name))
                .ToList();
        }
    }
}