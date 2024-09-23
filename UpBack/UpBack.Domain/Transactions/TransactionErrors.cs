using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Transactions
{
    // Listado de los posibles errores que pueden darse cuando
    public static class TransactionErrors
    {
        public static Error NotFound = new(
            "Transaction.NotFound",
            "Transaction not found");

        public static Error NotCompleted = new(
            "Transcaction.NotCompleted",
            "Only pending transactions can be completed.");

        public static Error NotFailed = new(
            "Transcaction.NotFailed",
            "Only pending transactions can be failed.");

        public static Error NotCancelled = new(
            "Transcaction.NotCancelled",
            "Only pending transactions can be cancelled.");

        public static Error NotRejected = new(
            "Transcaction.NotRejected",
            "Only pending transactions can be rejected.");

        public static Error NotRefunded = new(
            "Transcaction.NotRefunded",
            "Only completed transactions can be refunded.");

        public static readonly Error AccountNotFound = new(
            "Transcaction.AccountNotFound",
            "Account not found.");

        public static readonly Error GeneralFailure = new(
            "Transcaction.GeneralFailure",
            "An unexpected error occurred while processing the transcaction.");
    }
}
