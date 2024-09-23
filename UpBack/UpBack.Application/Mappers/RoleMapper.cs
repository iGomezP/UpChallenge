using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Roles;

namespace UpBack.Application.Mappers
{
    public static class RoleMapper
    {
        public static RoleDto MapToMongoDto(this Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Title = role.Title.Value,
                Permissions = role.Permissions.Select(p => new PermissionsDto
                {
                    Title = p.Title.Value,
                    Scope = p.Scope.Value,
                }),
                CreatedDate = role.CreatedDate,
                ObjectStatus = role.ObjectStatus,
            };
        }
    }
}
