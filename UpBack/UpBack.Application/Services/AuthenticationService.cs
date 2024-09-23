using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        public string GenerateAccessToken(CustomerDto user, RoleDto role)
        {
            var hours = 1;
            var minutes = 0;
            var expires = DateTime.UtcNow
                    .AddHours(hours)
                    .AddMinutes(minutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Guid", user.Id.ToString() ?? string.Empty),
                    new Claim("Name", $"{user.Name} {user.LastName}" ?? string.Empty),
                    new Claim("Email", user.Email ?? string.Empty),
                    new Claim("Role", role.Title ?? string.Empty),
                    new Claim("RoleId", role.Id.ToString() ?? string.Empty),
                    new Claim("Permissions", JsonSerializer.Serialize(role.Permissions.Select(p => $"{p.Title}.{p.Scope}".Replace(" ", "")).ToArray()), JsonClaimValueTypes.JsonArray),
                }),
                Expires = expires,
                Issuer = "https://localhost:7158/",
                Audience = "https://localhost:5173/",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("6M2U7FX9!mbT$zVqHjRtWp+LfK%xS3Ed")), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);

            return token;
        }
    }
}
