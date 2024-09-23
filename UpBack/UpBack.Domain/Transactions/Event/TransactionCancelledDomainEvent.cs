using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Transactions.Event
{
    public sealed record TransactionCancelledDomainEvent(Guid AccountId) : IDomainEvent;
}
