using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Accounts.Events
{
    public sealed record AccountCreatedDomainEvent(Guid AccountId, Guid CustomerId) : IDomainEvent;
}
