using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Accounts
{
    public static class AccountErrors
    {
        public static Error InsufficientBalance = new(
            "Accounts.InsufficientBalance",
            "Insufficient balance to perform the transaction.");

        public static Error BalanceNotZero = new(
            "Accounts.BalanceNotZero",
            "Account cannot be closed unless the balance is zero.");

        public static readonly Error GeneralFailure = new(
            "Account.GeneralFailure",
            "An unexpected error occurred while processing the account.");

        public static readonly Error NotFound = new(
            "Account.NotFound",
            "Account not found.");

        public static readonly Error AmountZero = new(
            "Account.AmountZero",
            "Deposit amount must be greater than zero.");

        public static Error Rejected = new(
            "Account.Rejected",
            "Transaction rejected.");
    }
}
