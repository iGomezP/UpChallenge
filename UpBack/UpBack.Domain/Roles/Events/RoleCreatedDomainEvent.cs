using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Roles.Events
{
    public sealed record RoleCreatedDomainEvent(Guid RoleId) : IDomainEvent;
}
