using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Customers.Events
{
    public sealed record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;
}
