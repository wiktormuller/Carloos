using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Countries.Queries
{
    public class GetCountryByIdQuery : IRequest<CountryResponse>
    {
        public int Id { get; private set; }

        public GetCountryByIdQuery(int id)
        {
            Id = id;
        }
    }
}