﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Users.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.QueriesHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll();

            return users
                .Select(user => new UserResponse(user.Id, user.UserName, user.Email))
                .ToList();
        }
    }
}