using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Transactions.Event
{
    public sealed record TransactionRejectedDomainEvent(Guid AccountId) : IDomainEvent;
}
