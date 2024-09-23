using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Transactions.Event
{
    public sealed record TransactionFailedDomainEvent(Guid AccountId) : IDomainEvent;
}
