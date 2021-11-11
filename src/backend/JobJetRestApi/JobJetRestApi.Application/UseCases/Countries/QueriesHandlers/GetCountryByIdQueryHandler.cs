using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.QueriesHandlers
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryResponse>
    {
        public Task<CountryResponse> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new CountryResponse();

            return Task.FromResult(result);
        }
    }
}