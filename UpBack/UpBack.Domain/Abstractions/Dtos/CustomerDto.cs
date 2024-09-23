using System.Text.Json.Serialization;

namespace UpBack.Domain.Abstractions.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public BirthDayDto BirthDay { get; set; }
        public AddressDto Address { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ObjectStatus { get; set; }
    }

    public class BirthDayDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public static DateOnly ToDateOnly(BirthDayDto dto)
        {
            return new DateOnly(dto.Year, dto.Month, dto.Day);
        }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
