using UpBack.Domain.Abstractions;
using UpBack.Domain.Roles;

namespace UpBack.Domain.ObjectValues
{
    public partial record Title
    {
        public Title()
        {

        }
        public string Value { get; }

        private Title(string value) => Value = value;

        public static Result<Title> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<Title>(RoleErrors.NullTitle);
            }

            return Result.Success(new Title(value));
        }

        public override string ToString() => Value;
    }
}
