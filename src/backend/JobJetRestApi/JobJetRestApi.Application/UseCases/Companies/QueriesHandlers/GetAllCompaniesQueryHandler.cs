using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Companies.Queries;
using MediatR;
using System.Linq;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Application.UseCases.Companies.QueriesHandlers
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<CompanyResponse>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMemoryCache _memoryCache;

        public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository, 
            IMemoryCache memoryCache)
        {
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
            _companyRepository = Guard.Against.Null(companyRepository, nameof(companyRepository));
        }

        public async Task<List<CompanyResponse>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "companiesKey";
            
            if (!_memoryCache.TryGetValue(cacheKey, out List<Domain.Entities.Company> companies))
            {
                companies = await _companyRepository.GetAll();
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(cacheKey, companies, cacheExpiryOptions);
            }
            
            return companies
                .Select(x => new CompanyResponse(x.Id, x.Name, x.ShortName, x.Description, x.NumberOfPeople, x.CityName))
                .ToList();
        }
    }
}