using System.Text.RegularExpressions;

namespace UpBack.Domain.ObjectValues
{
    public partial record PhoneNumber
    {
        public PhoneNumber()
        {

        }
        public string Value { get; }
        private PhoneNumber(string value) => Value = value;

        public static PhoneNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Phone number cannot be null or empty.", nameof(value));
            }

            if (value.Length < 7 || value.Length > 15)
            {
                throw new ArgumentException("Phone number must be between 7 and 15 characters long.", nameof(value));
            }

            if (!PhoneNumberRegex().IsMatch(value))
            {
                throw new ArgumentException("Phone number can only contain digits and an optional leading '+'.", nameof(value));
            }

            return new PhoneNumber(value);
        }

        public override string ToString() => Value;

        [GeneratedRegex(@"^\+?[0-9]{7,15}$", RegexOptions.Compiled)]
        private static partial Regex PhoneNumberRegex();
    }
}

