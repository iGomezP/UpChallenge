using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Customers;

namespace UpBack.Application.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDto MapToMongoDto(this Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name.Value,
                LastName = customer.LastName.Value,
                Password = customer.Password.ToString(),
                Email = customer.Email.Value,
                PhoneNumber = customer.PhoneNumber.Value,
                BirthDay = new BirthDayDto
                {
                    Year = customer.BirthDay.Year,
                    Month = customer.BirthDay.Month,
                    Day = customer.BirthDay.Day
                },
                Address = new AddressDto
                {
                    Street = customer.Address.Street,
                    City = customer.Address.City,
                    State = customer.Address.State,
                    ZipCode = customer.Address.ZipCode,
                    Country = customer.Address.Country
                },
                RoleId = customer.RoleId,
                CreatedDate = customer.CreatedDate,
                ObjectStatus = customer.ObjectStatus
            };
        }
    }
}
