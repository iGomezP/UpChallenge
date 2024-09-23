using System.Text.RegularExpressions;

namespace UpBack.Domain.ObjectValues
{
    public partial record AccountNumber
    {
        public AccountNumber()
        {

        }
        public string Value { get; }
        private AccountNumber(string value) => Value = value;

        public static AccountNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Account number cannot be null or empty.", nameof(value));
            }


            if (value.Length < 10 || value.Length > 20)
            {
                throw new ArgumentException("Account number must be between 10 and 20 characters long.", nameof(value));
            }


            if (!AccountNumberRegex().IsMatch(value))
            {
                throw new ArgumentException("Account number can only contain digits.", nameof(value));
            }

            return new AccountNumber(value);
        }


        public override string ToString() => Value;

        [GeneratedRegex(@"^\d+$", RegexOptions.Compiled)]
        private static partial Regex AccountNumberRegex();
    }
}

