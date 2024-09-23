namespace UpBack.Application.Common
{
    public sealed class TransactionResponse
    {
        public Guid Id { get; init; }
        public string TransactionType { get; init; }
        public decimal Quantity { get; init; }
        public string Reference { get; init; }
        public string Status { get; init; }
        public DateTime TransactionDate { get; init; }
        public Guid AccountId { get; init; }
    }
}
