namespace UpBack.Domain.ObjectValues
{
    public partial record TransactionType
    {
        public TransactionType()
        {

        }
        public string Value { get; }
        private TransactionType(string value) => Value = value;
        private static readonly string[] AllowedTransactionTypes = { "Deposit", "Withdrawal", "Transfer" };

        public static TransactionType Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Transaction type cannot be null or empty.", nameof(value));
            }
            if (!AllowedTransactionTypes.Contains(value))
            {
                throw new ArgumentException($"Invalid transaction type. Allowed values are: {string.Join(", ", AllowedTransactionTypes)}", nameof(value));
            }

            return new TransactionType(value);
        }

        public override string ToString() => Value;
    }
}

