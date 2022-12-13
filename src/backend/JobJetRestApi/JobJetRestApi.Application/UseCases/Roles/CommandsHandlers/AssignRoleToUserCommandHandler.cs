using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Roles.Commands;
using JobJetRestApi.Domain.Repositories;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Roles.CommandsHandlers;

public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    
    public AssignRoleToUserCommandHandler(IRoleRepository roleRepository,
        IUserRepository userRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            throw UserNotFoundException.ForId(request.UserId);
        }
        
        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        if (role is null)
        {
            throw RoleNotFoundException.ForId(request.RoleId);
        }

        await _userRepository.AssignRoleToUser(user, role);

        return Unit.Value;
    }
}