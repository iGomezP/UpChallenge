using System.Security.Cryptography;
using System.Text;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Customers;

namespace UpBack.Domain.ObjectValues
{
    public partial record Password
    {
        public Password()
        {

        }
        private const int MinLength = 8;
        public string HashedPassword { get; }

        // Para evitar instacias sin validación
        private Password(string hashedPassword)
        {
            HashedPassword = hashedPassword;
        }

        public static Result<Password> Create(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < MinLength)
            {
                return Result.Failure<Password>(CustomerErrors.InvalidLengthPassword);
            }

            string hashedPassword = HashPassword(password);
            return Result.Success(new Password(hashedPassword));
        }

        public bool Verify(string password)
        {
            return VerifyHashedPassword(HashedPassword, password);
        }

        private static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            // Compara el hash almacenado con el hash de la entrada
            return hashedPassword == HashPassword(password);
        }

        // Sobreescribir ToString para no exponer el valor sensible
        public override string ToString()
        {
            return HashedPassword;
        }
    }
}
