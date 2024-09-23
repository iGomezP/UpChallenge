using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Accounts.Events
{
    public sealed record AccountDeletedDomainEvent(Guid AccountId) : IDomainEvent;
}
