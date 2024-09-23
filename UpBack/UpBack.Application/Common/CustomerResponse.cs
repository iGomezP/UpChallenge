namespace UpBack.Application.Common
{
    public sealed class CustomerResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime BirthDay { get; init; }
        public CustomerAddressResponse AddressResponse { get; init; }
        public DateTime CreatedDate { get; init; }
        public string ObjectStatus { get; init; }
    }

    public sealed class CustomerAddressResponse
    {
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
        public string Country { get; init; }
    }
}
