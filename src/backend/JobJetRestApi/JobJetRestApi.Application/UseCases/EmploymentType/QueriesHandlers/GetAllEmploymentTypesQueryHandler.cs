using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Application.UseCases.EmploymentType.QueriesHandlers
{
    public class GetAllEmploymentTypesQueryHandler : IRequestHandler<GetAllEmploymentTypesQuery, List<EmploymentTypeResponse>>
    {
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        private readonly IMemoryCache _memoryCache;
        
        public GetAllEmploymentTypesQueryHandler(IEmploymentTypeRepository employmentTypeRepository, 
            IMemoryCache memoryCache)
        {
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
            _employmentTypeRepository = Guard.Against.Null(employmentTypeRepository, nameof(employmentTypeRepository));
        }

        public async Task<List<EmploymentTypeResponse>> Handle(GetAllEmploymentTypesQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "employmentTypesKey";

            if (!_memoryCache.TryGetValue(cacheKey, out List<Domain.Entities.EmploymentType> employmentTypes))
            {
                employmentTypes = await _employmentTypeRepository.GetAllAsync();
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(cacheKey, employmentTypes, cacheExpiryOptions);
            }

            return employmentTypes
                .Select(x => new EmploymentTypeResponse(x.Id, x.Name))
                .ToList();
        }
    }
}