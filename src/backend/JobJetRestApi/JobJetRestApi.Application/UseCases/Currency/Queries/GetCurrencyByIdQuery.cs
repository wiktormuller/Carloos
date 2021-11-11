using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.Queries
{
    public class GetCurrencyByIdQuery : IRequest<CurrencyResponse>
    {
        public int Id { get; private set; }
        
        public GetCurrencyByIdQuery(int id)
        {
            Id = id;
        }
    }
}