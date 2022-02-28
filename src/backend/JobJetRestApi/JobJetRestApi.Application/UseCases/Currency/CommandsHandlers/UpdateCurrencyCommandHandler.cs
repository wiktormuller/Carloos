using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
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
            _currencyRepository = Guard.Against.Null(currencyRepository, nameof(currencyRepository));
        }

        /// <exception cref="CurrencyNotFoundException"></exception>
        /// <exception cref="CurrencyAlreadyExistsException"></exception>
        public async Task<Unit> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (! await _currencyRepository.Exists(request.Id))
            {
                throw CurrencyNotFoundException.ForId(request.Id);
            }

            if (await _currencyRepository.Exists(request.Name))
            {
                throw CurrencyAlreadyExistsException.ForName(request.Name);
            }

            var currency = await _currencyRepository.GetById(request.Id);
            currency.UpdateName(request.Name);

            await _currencyRepository.Update();
            
            return Unit.Value;
        }
    }
}