using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Customers.Events
{
    public sealed record CustomerDeletedDomainEvent(Guid CustomerId) : IDomainEvent;
}
