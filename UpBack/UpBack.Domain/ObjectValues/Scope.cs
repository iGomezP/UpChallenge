using UpBack.Domain.Abstractions;
using UpBack.Domain.Roles;

namespace UpBack.Domain.ObjectValues
{
    public partial record Scope
    {
        public Scope()
        {

        }
        public string Value { get; }
        private Scope(string value) => Value = value;

        public static Result<Scope> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<Scope>(RoleErrors.NullScope);
            }

            return Result.Success(new Scope(value));
        }

        public override string ToString() => Value;
    }
}
