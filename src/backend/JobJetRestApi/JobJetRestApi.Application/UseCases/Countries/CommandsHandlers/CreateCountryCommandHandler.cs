using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Countries.Commands;
using JobJetRestApi.Domain.Entities;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.CommandsHandlers
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly ICountryRepository _countryRepository;
        
        public CreateCountryCommandHandler(ICountryRepository countryRepository)
        {
            _countryRepository = Guard.Against.Null(countryRepository, nameof(countryRepository));
        }
        
        /// <exception cref="CountryAlreadyExistsException"></exception>
        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            if (await _countryRepository.Exists(request.Name, request.Alpha2Code, request.Alpha2Code, request.NumericCode))
            {
                throw CountryAlreadyExistsException.ForName(request.Name);
            }

            var country = new Country(request.Name, request.Alpha2Code, request.Alpha3Code, request.NumericCode);
            await _countryRepository.Create(country);

            return country.Id;
        }
    }
}