using UpBack.Domain.Abstractions;
using UpBack.Domain.Customers.Events;
using UpBack.Domain.ObjectValues;

namespace UpBack.Domain.Customers
{
    public sealed class Customer : Entity
    {
        private Customer()
        {

        }

        // Ocultamos la logica del ctor, puede tener detalles del diseño y no se quiere exponer a agentes externos
        private Customer(
            Guid id,
            Name name,
            LastName lastName,
            CustomerEmail email,
            PhoneNumber phoneNumber,
            DateOnly birthDay,
            Address address,
            Password password,
            DateTime createdDate,
            string objectStatus
            ) : base(id)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDay = birthDay;
            Address = address;
            Password = password;
            CreatedDate = createdDate;
            ObjectStatus = objectStatus;
        }

        public Name Name { get; private set; }
        public LastName LastName { get; private set; }
        public CustomerEmail Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public DateOnly BirthDay { get; private set; }
        public Address Address { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string ObjectStatus { get; private set; } = "active";
        public Guid RoleId { get; private set; } = Guid.Parse("4ae0ee8f-dcf1-46fa-9f66-1c4e37639638");
        // añadir campo contraseña
        public Password Password { get; private set; }


        // Ejemplo de encapsulacion
        public static Customer Create(
            Name name,
            LastName lastName,
            CustomerEmail email,
            PhoneNumber phoneNumber,
            DateOnly birthDay,
            Address address,
            Password password,
            DateTime createdDate,
            string objectStatus
            )
        {
            if (!IsValidAge(birthDay))
            {
                throw new ArgumentException("Customer must be at least 18 years old.", nameof(birthDay));
            }

            var customer = new Customer(
                Guid.NewGuid(),
                name,
                lastName,
                email,
                phoneNumber,
                birthDay,
                address,
                password,
                createdDate,
                objectStatus);

            // Publicamos el evento cada vez que se crea un customer
            customer.RaiseDomainEvent(new CustomerCreatedDomainEvent(customer.Id));

            return customer;
        }

        public Result UpdateContactInfo(Address newAddress, PhoneNumber newPhoneNumber)
        {
            if (newAddress == null)
            {
                return Result.Failure(CustomerErrors.NullAddress);
            }

            if (newPhoneNumber == null)
            {
                return Result.Failure(CustomerErrors.NullPhone);
            }

            // Actualizar solo la dirección y el teléfono
            Address = newAddress;
            PhoneNumber = newPhoneNumber;

            // Disparar un evento de dominio si es necesario
            RaiseDomainEvent(new CustomerContactInfoUpdatedDomainEvent(Id, newAddress, newPhoneNumber));

            return Result.Success();
        }

        public Result Delete()
        {
            ObjectStatus = "inactive";
            RaiseDomainEvent(new CustomerDeletedDomainEvent(Id));
            return Result.Success();
        }


        private static bool IsValidAge(DateOnly birthDay)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - birthDay.Year;

            if (birthDay > today.AddYears(-age)) // Si el cumpleaños aún no ha ocurrido este año
            {
                age--;
            }

            return age >= 18;
        }
    }
}
