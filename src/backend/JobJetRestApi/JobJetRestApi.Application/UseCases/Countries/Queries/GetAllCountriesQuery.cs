using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.Queries
{
    public class GetAllCountriesQuery : IRequest<List<CountryResponse>>
    {
        
    }
}