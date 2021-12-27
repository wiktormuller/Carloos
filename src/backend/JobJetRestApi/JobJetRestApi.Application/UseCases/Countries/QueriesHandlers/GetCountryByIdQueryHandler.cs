using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.QueriesHandlers
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryResponse>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountryByIdQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<CountryResponse> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            if (! await _countryRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            var country = await _countryRepository.GetById(request.Id);

            var result = new CountryResponse(country.Id, country.Name, country.Alpha2Code);

            return result;
        }
    }
}