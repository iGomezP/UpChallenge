using UpBack.Domain.Abstractions;

namespace UpBack.Domain.ObjectValues
{
    public record AccountBalance
    {
        public AccountBalance()
        {

        }
        public decimal Value { get; }
        private AccountBalance(decimal value) => Value = value;

        public static Result<AccountBalance> Create(decimal value)
        {
            if (value < 0)
            {
                throw new InvalidOperationException("Account balance must be greater than zero to perform a transaction.");
            }

            if (decimal.Round(value, 2) != value)
            {
                throw new InvalidOperationException("Account balance can only have up to two decimal places.");
            }

            return Result.Success(new AccountBalance(value));
        }

        public override string ToString() => Value.ToString("F2");
    }
}
