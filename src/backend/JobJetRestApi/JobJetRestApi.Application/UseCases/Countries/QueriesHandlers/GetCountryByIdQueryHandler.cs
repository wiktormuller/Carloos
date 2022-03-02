using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.QueriesHandlers
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryResponse>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountryByIdQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = Guard.Against.Null(countryRepository, nameof(countryRepository));
        }

        public async Task<CountryResponse> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            if (! await _countryRepository.ExistsAsync(request.Id))
            {
                throw CountryNotFoundException.ForId(request.Id);
            }

            var country = await _countryRepository.GetByIdAsync(request.Id);

            var result = new CountryResponse(country.Id, country.Name, country.Alpha2Code);

            return result;
        }
    }
}