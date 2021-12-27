using System;
using System.Threading;
using System.Threading.Tasks;
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
            _countryRepository = countryRepository;
        }

        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            if (await _countryRepository.Exists(request.Name, request.Alpha2Code, request.Alpha2Code, request.NumericCode))
            {
                throw new ArgumentException(nameof(request.Name));
                // @TODO - Throw Domain Exception
            }

            var country = new Country(request.Name, request.Alpha2Code, request.Alpha3Code, request.NumericCode);
            await _countryRepository.Create(country);

            return country.Id;
        }
    }
}