using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Roles
{
    public static class RoleErrors
    {
        public static Error NullTitle = new(
            "Rol.NullTitle",
            "Title cannot be null or empty."
            );

        public static Error NullPermissions = new(
            "Rol.NullPermissions",
            "Permissions cannot be null."
            );

        public static Error NullScope = new(
            "Rol.NullScope",
            "Scope cannot be null or empty."
            );

        public static Error NotFound = new(
            "Rol.NotFound",
            "Role not found."
            );
    }
}
