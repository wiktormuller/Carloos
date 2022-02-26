using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public int Id { get; private set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}