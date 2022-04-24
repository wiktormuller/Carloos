using MediatR;

namespace JobJetRestApi.Application.UseCases.Roles.Commands;

public class AssignRoleToUserCommand : IRequest
{
    public int RoleId { get; private set; }
    public int UserId { get; private set; }

    public AssignRoleToUserCommand(int roleId, int userId)
    {
        RoleId = roleId;
        UserId = userId;
    }
}