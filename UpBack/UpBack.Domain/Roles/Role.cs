using UpBack.Domain.Abstractions;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Permissions;
using UpBack.Domain.Roles.Events;

namespace UpBack.Domain.Roles
{
    public sealed class Role : Entity
    {
        public Role() { }

        private Role(
            Guid id,
            Title title,
            IEnumerable<Permission> permissions,
            DateTime createdDate,
            string objectStatus
            ) : base(id)
        {
            Title = title;
            Permissions = permissions;
            CreatedDate = createdDate;
            ObjectStatus = objectStatus;
        }

        public Title Title { get; private set; }
        public IEnumerable<Permission> Permissions { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string ObjectStatus { get; private set; } = "active";

        public static Role Create(
            Title title,
            IEnumerable<Permission> permissions,
            DateTime createdDate,
            string objectStatus
            )
        {
            var role = new Role(
                Guid.NewGuid(),
                title,
                permissions,
                createdDate,
                objectStatus);

            role.RaiseDomainEvent(new RoleCreatedDomainEvent(role.Id));

            return role;
        }

        public Result Update(
            Title title,
            IEnumerable<Permission> permissions
            )
        {
            if (title == null)
            {
                return Result.Failure(RoleErrors.NullTitle);
            }

            if (permissions == null)
            {
                return Result.Failure(RoleErrors.NullPermissions);
            }

            Title = title;
            Permissions = permissions;

            return Result.Success();
        }

        public Result Delete()
        {
            ObjectStatus = "inactive";
            return Result.Success();
        }
    }
}
