using UpBack.Domain.Transactions;

namespace UpBack.Domain.Abstractions.Dtos
{
    public sealed class TransactionDto
    {
        public Guid Id { get; init; }
        public Guid AccountId { get; init; }
        public string TransactionType { get; init; }
        public decimal Quantity { get; init; }
        public DateTime TransactionDate { get; init; }
        public string Reference { get; init; }
        public TransactionStatusEnum Status { get; init; }
    }
}
