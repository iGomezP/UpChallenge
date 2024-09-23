using System.Text.RegularExpressions;

namespace UpBack.Domain.ObjectValues
{
    public partial record Name
    {
        public Name()
        {

        }
        public string Value { get; }
        private Name(string value) => Value = value;

        public static Name Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(value));
            }

            if (value.Length < 2 || value.Length > 50)
            {
                throw new ArgumentException("Name must be between 2 and 50 characters long.", nameof(value));
            }

            if (!NameRegex().IsMatch(value))
            {
                throw new ArgumentException("Name can only contain letters and spaces.", nameof(value));
            }

            return new Name(value);
        }
        public override string ToString() => Value;

        [GeneratedRegex(@"^[a-zA-Z\s]+$", RegexOptions.Compiled)]
        private static partial Regex NameRegex();
    }
}
