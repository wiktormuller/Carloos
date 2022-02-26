using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Users.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Users.QueriesHandlers
{
    public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.Exists(request.Id))
            {
                throw UserNotFoundException.ForId(request.Id);
            }

            var user = await _userRepository.GetById(request.Id);

            var result = new UserResponse(user.Id, user.UserName, user.Email);

            return result;
        }
    }
}