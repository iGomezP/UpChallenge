using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Services.Roles.Querys.GetById
{
    public sealed record GetRoleByIdQuery(Guid RoleId) : IQuery<RoleDto>;
}
