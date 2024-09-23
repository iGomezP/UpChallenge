using UpBack.Domain.Abstractions;
using UpBack.Domain.ObjectValues;

namespace UpBack.Domain.Customers.Events
{
    public sealed record CustomerContactInfoUpdatedDomainEvent(Guid CustomerId, Address NewAddress, PhoneNumber NewPhoneNumber) : IDomainEvent;
}
