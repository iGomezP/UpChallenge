using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Transactions.Event
{
    public sealed record TransactionCompletedDomainEvent(Guid AccountId) : IDomainEvent;
}
