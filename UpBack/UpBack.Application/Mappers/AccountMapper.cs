using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Accounts;

namespace UpBack.Application.Mappers
{
    public static class AccountMapper
    {
        public static AccountDto MapToMongoDto(this Account account)
        {
            return new AccountDto
            {
                Id = account.Id,
                AccountNumber = account.Number.Value,
                Balance = account.Balance.Value,
                ObjectStatus = account.ObjectStatus,
                CreatedDate = account.CreatedDate,
                Customer = new CustomerDto
                {
                    Id = account.Customer.Id,
                    Name = account.Customer.Name.Value,
                    LastName = account.Customer.LastName.Value,
                    Email = account.Customer.Email.Value,
                    RoleId = account.Customer.RoleId,
                    PhoneNumber = account.Customer.PhoneNumber.Value,
                    BirthDay = new BirthDayDto
                    {
                        Year = account.Customer.BirthDay.Year,
                        Month = account.Customer.BirthDay.Month,
                        Day = account.Customer.BirthDay.Day
                    },
                    Address = new AddressDto
                    {
                        Street = account.Customer.Address.Street,
                        City = account.Customer.Address.City,
                        State = account.Customer.Address.State,
                        ZipCode = account.Customer.Address.ZipCode,
                        Country = account.Customer.Address.Country
                    },
                    CreatedDate = account.Customer.CreatedDate,
                    ObjectStatus = account.Customer.ObjectStatus
                }
            };
        }
    }
}
