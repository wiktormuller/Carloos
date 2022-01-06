using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.Queries
{
    public class GetAllCurrenciesQuery : IRequest<List<CurrencyResponse>>
    {
        
    }
}