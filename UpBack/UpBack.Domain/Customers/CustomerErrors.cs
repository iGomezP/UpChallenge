using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Customers
{
    public static class CustomerErrors
    {
        public static Error NotFound = new(
            "User.NotFound",
            "The user with the specified ID does not exist.");

        public static Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The username or password is incorrect.");

        public static Error DuplicateEmail = new(
            "User.DuplicateEmail",
            "A customer with this email already exists.");

        public static Error NullAddress = new(
            "User.NullAddress",
            "Address cannot be null.");

        public static Error NullPhone = new(
            "User.NullPhone",
            "Phone number cannot be null.");

        public static readonly Error GeneralFailure = new(
            "User.GeneralFailure",
            "An unexpected error occurred while processing the user.");

        public static readonly Error InvalidLengthPassword = new(
            "User.InvalidPassword",
            $"Password must be at least 8 characters long.");

        public static readonly Error InvalidPassword = new(
            "User.InvalidPassword",
            $"Invalid email or password.");
    }
}
