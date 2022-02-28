using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Application.UseCases.JobOffers.QueriesHandlers
{
    public class GetAllJobOffersQueryHandler : IRequestHandler<GetAllJobOffersQuery, List<JobOfferResponse>>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IMemoryCache _memoryCache;
        
        public GetAllJobOffersQueryHandler(IJobOfferRepository jobOfferRepository, 
            IMemoryCache memoryCache)
        {
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
            _jobOfferRepository = Guard.Against.Null(jobOfferRepository, nameof(jobOfferRepository));
        }

        public async Task<List<JobOfferResponse>> Handle(GetAllJobOffersQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "jobOffersKey";

            if (!_memoryCache.TryGetValue(cacheKey, out List<Domain.Entities.JobOffer> jobOffers))
            {
                jobOffers = await _jobOfferRepository.GetAll();
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(cacheKey, jobOffers, cacheExpiryOptions);
            }

            return jobOffers
                .Select(x => new JobOfferResponse(
                    x.Id, x.Name, x.Description, x.SalaryFrom, x.SalaryTo, x.Address.Id, x.Address.Country.Name,
                    x.Address.Town, x.Address.Street, x.Address.ZipCode, x.Address.Latitude, x.Address.Longitude, x.TechnologyType.Name, 
                    x.Seniority.Name, x.EmploymentType.Name))
                .ToList();
        }
    }
}