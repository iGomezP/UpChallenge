using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Permissions
{
    public static class PermissionErrors
    {
        public static Error NullTitle = new(
            "Permission.NullTitle",
            "Title cannot be null."
            );

        public static Error NullScope = new(
            "Permission.NullTitle",
            "Scope cannot be null."
            );
    }
}
