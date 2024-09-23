using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Services
{
    public interface IAuthenticationService
    {
        string GenerateAccessToken(CustomerDto user, RoleDto role);
    }
}
