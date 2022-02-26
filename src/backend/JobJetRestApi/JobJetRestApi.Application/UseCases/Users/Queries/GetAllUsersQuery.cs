using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserResponse>>
    {
        
    }
}