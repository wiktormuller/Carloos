using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Countries.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.CommandsHandlers
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand>
    {
        private readonly ICountryRepository _countryRepository;

        public UpdateCountryCommandHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            if (!await _countryRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            if (await _countryRepository.Exists(request.Name))
            {
                throw new ArgumentException(nameof(request.Name));
                // @TODO - Throw Domain Exception
            }

            var country = await _countryRepository.GetById(request.Id);
            country.UpdateName(request.Name);

            await _countryRepository.Update();
            
            return Unit.Value;
        }
    }
}