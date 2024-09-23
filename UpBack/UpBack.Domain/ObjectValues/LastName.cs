using System.Text.RegularExpressions;

namespace UpBack.Domain.ObjectValues
{
    public partial record LastName
    {
        public LastName()
        {

        }
        public string Value { get; }
        private LastName(string value) => Value = value;


        public static LastName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Last name cannot be null or empty.", nameof(value));
            }
            if (value.Length < 2 || value.Length > 50)
            {
                throw new ArgumentException("Last name must be between 2 and 50 characters long.", nameof(value));
            }
            if (!LastNameRegex().IsMatch(value))
            {
                throw new ArgumentException("Last name can only contain letters and spaces.", nameof(value));
            }

            return new LastName(value);
        }

        public override string ToString() => Value;

        [GeneratedRegex(@"^[a-zA-Z\s]+$", RegexOptions.Compiled)]
        private static partial Regex LastNameRegex();
    }
}

