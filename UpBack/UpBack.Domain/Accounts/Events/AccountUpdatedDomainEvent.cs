using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Accounts.Events
{
    public sealed record AccountUpdatedDomainEvent(Guid AccountId, decimal NewBalance, string ObjectStatus) : IDomainEvent;
}
