using MediatR;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Permissions;

namespace UpBack.Application.Services.Roles.Commands.CreateRolecommand
{
    public sealed record CreateRoleCommand(
        string Title,
        IEnumerable<Permission> Permissions,
        string ObjectStatus
    ) : IRequest<Result<Guid>>;
}
