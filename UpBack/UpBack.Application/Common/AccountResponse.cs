namespace UpBack.Application.Common
{
    public sealed class AccountResponse
    {
        public Guid Id { get; init; }
        public string AccountNumber { get; init; }
        public decimal Balance { get; init; }
        public string ObjectStatus { get; init; }
        public DateTime CreatedDate { get; init; }
        public Guid CustomerId { get; init; }
        public CustomerResponse Customer { get; init; }
    }
}
