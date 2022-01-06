using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.QueriesHandlers
{
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, List<CountryResponse>>
    {
        private readonly ICountryRepository _countryRepository;
        
        public GetAllCountriesQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        
        public async Task<List<CountryResponse>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.GetAll();

            return countries
                .Select(x => new CountryResponse(x.Id, x.Name, x.Alpha2Code))
                .ToList();
        }
    }
}