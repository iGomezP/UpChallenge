using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Transactions.Event
{
    public sealed record TransactionRefundedDomainEvent(Guid AccountId) : IDomainEvent;
}
