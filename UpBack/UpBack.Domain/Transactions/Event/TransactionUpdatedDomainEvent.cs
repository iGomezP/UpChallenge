using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Transactions.Event
{
    public sealed record TransactionUpdatedDomainEvent(Guid TransactionId, Guid AccountId) : IDomainEvent;
}
