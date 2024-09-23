using System.Text.RegularExpressions;

namespace UpBack.Domain.ObjectValues
{
    public partial record CustomerEmail
    {
        public CustomerEmail()
        {

        }
        private CustomerEmail(string value) => Value = value;

        public string Value { get; }

        public static CustomerEmail Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(value));
            }

            if (!GenerateEmailRegex().IsMatch(value))
            {
                throw new ArgumentException("Invalid email format.", nameof(value));
            }

            if (value.Length > 254)
            {
                throw new ArgumentException("Email cannot be longer than 254 characters.", nameof(value));
            }

            return new CustomerEmail(value);
        }

        public override string ToString() => Value;

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled)]
        private static partial Regex GenerateEmailRegex();
    }
}
