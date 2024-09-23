using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Transactions.Event
{
    // EL evento en si necesita tener un id para diferenciarlo si es duplicado *idempotency
    public sealed record TransactionCreatedDomainEvent(Guid TransactionId) : IDomainEvent;
}
