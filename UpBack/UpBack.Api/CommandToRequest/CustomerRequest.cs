using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Api.CommandToRequest
{
    public sealed record CustomerRequest(
        string Name,
        string Password,
        string LastName,
        string Email,
        string PhoneNumber,
        BirthDayDto BirthDay,
        string Street,
        string City,
        string ZipCode,
        string Country,
        string State
        );
}
