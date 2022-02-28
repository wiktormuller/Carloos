using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Countries.Commands;
using MediatR;
using JobJetRestApi.Application.Exceptions;

namespace JobJetRestApi.Application.UseCases.Countries.CommandsHandlers
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand>
    {
        private readonly ICountryRepository _countryRepository;

        public UpdateCountryCommandHandler(ICountryRepository countryRepository)
        {
            _countryRepository = Guard.Against.Null(countryRepository, nameof(countryRepository));
        }

        /// <exception cref="CountryNotFoundException"></exception>
        /// <exception cref="CountryAlreadyExistsException"></exception>
        public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            if (!await _countryRepository.Exists(request.Id))
            {
                throw CountryNotFoundException.ForId(request.Id);
            }

            if (await _countryRepository.Exists(request.Name))
            {
                throw CountryAlreadyExistsException.ForName(request.Name);
            }

            var country = await _countryRepository.GetById(request.Id);
            country.UpdateName(request.Name);

            await _countryRepository.Update();
            
            return Unit.Value;
        }
    }
}