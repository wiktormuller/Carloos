using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Countries.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.CommandsHandlers
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        private readonly ICountryRepository _countryRepository;
        
        public DeleteCountryCommandHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            if (!await _countryRepository.Exists(request.Id))
            {
                throw CountryNotFoundException.ForId(request.Id);
            }

            var country = await _countryRepository.GetById(request.Id);
            await _countryRepository.Delete(country);
            
            return Unit.Value;
        }
    }
}