namespace UpBack.Domain.ObjectValues
{
    public record TransactionQuantity
    {
        public TransactionQuantity()
        {

        }
        public decimal Value { get; }
        private TransactionQuantity(decimal value) => Value = value;
        public static TransactionQuantity Create(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Transaction quantity must be greater than zero.", nameof(value));
            }

            if (decimal.Round(value, 2) != value)
            {
                throw new ArgumentException("Transaction quantity must have at most two decimal places.", nameof(value));
            }

            return new TransactionQuantity(value);
        }

        public override string ToString() => Value.ToString("F2");
    }
}

