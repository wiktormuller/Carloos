using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Currency.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.CommandsHandlers
{
    public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;
        
        public UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<Unit> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (! await _currencyRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            if (await _currencyRepository.Exists(request.Name))
            {
                throw new ArgumentException(nameof(request.Name));
                // @TODO - Throw Domain Exception
            }

            var currency = await _currencyRepository.GetById(request.Id);
            currency.UpdateName(request.Name);

            await _currencyRepository.Update();
            
            return Unit.Value;
        }
    }
}