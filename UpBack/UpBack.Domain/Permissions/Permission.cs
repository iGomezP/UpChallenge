using UpBack.Domain.Abstractions;
using UpBack.Domain.ObjectValues;

namespace UpBack.Domain.Permissions
{
    public sealed class Permission : Entity
    {
        public Permission() { }

        public Permission(
            Guid id,
            Title title,
            Scope scope,
            DateTime createdDate,
            string objectStatus
            ) : base(id)
        {
            Title = title;
            Scope = scope;
            CreatedDate = createdDate;
            ObjectStatus = objectStatus;
        }

        public Title Title { get; private set; }
        public Scope Scope { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string ObjectStatus { get; private set; } = "active";

        public static Permission Create(
            Title title,
            Scope scope,
            DateTime createdDate,
            string objectStatus
            )
        {
            return new Permission(
                Guid.NewGuid(),
                title,
                scope,
                createdDate,
                objectStatus);
        }

        public Result Update(
            Title title,
            Scope scope
            )
        {
            if (title == null)
            {
                return Result.Failure(PermissionErrors.NullTitle);
            }

            if (scope == null)
            {
                return Result.Failure(PermissionErrors.NullScope);
            }

            Title = title;
            Scope = scope;


            return Result.Success();
        }

        public Result Delete()
        {
            ObjectStatus = "inactive";
            return Result.Success();
        }
    }
}
