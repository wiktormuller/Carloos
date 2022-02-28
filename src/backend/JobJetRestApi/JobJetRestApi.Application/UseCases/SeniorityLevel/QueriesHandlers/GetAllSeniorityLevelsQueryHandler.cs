using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Application.UseCases.SeniorityLevel.QueriesHandlers
{
    public class GetAllSeniorityLevelsQueryHandler : IRequestHandler<GetAllSeniorityLevelsQuery, List<SeniorityLevelResponse>>
    {
        private readonly ISeniorityRepository _seniorityRepository;
        private readonly IMemoryCache _memoryCache;
        
        public GetAllSeniorityLevelsQueryHandler(ISeniorityRepository seniorityRepository, 
            IMemoryCache memoryCache)
        {
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
            _seniorityRepository = Guard.Against.Null(seniorityRepository, nameof(seniorityRepository));
        }

        public async Task<List<SeniorityLevelResponse>> Handle(GetAllSeniorityLevelsQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "seniorityLevelsKey";

            if (!_memoryCache.TryGetValue(cacheKey, out List<Domain.Entities.Seniority> seniorityLevels))
            {
                seniorityLevels = await _seniorityRepository.GetAll();
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(cacheKey, seniorityLevels, cacheExpiryOptions);
            }

            return seniorityLevels
                .Select(x => new SeniorityLevelResponse(x.Id, x.Name))
                .ToList();
        }
    }
}