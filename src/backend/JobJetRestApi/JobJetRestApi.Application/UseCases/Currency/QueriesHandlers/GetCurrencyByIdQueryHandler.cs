using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.QueriesHandlers
{
    public class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, CurrencyResponse>
    {
        public Task<CurrencyResponse> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new CurrencyResponse();

            return Task.FromResult(result);
        }
    }
}