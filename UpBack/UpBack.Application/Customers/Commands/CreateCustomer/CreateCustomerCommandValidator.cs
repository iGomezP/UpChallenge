using FluentValidation;

namespace UpBack.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            // Validación para Name: No vacío y longitud entre 2 y 50 caracteres
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

            // Validación para LastName: No vacío y longitud entre 2 y 50 caracteres
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

            // Validación para Email: No vacío y debe tener un formato válido
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // Validación para PhoneNumber: No vacío y longitud mínima de 7 caracteres (dependiendo del país)
            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Phone number must contain only digits and have a valid format.");

            // Validación para BirthDay: Debe ser mayor de 18 años
            RuleFor(c => c.BirthDay)
                .Must(BeAValidAge).WithMessage("Customer must be at least 18 years old.");

            // Validación para Address: Cada campo debe ser válido
            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("Street is required.")
                .Length(2, 100).WithMessage("Street must be between 2 and 100 characters.");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("City is required.")
                .Length(2, 50).WithMessage("City must be between 2 and 50 characters.");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.")
                .Length(4, 10).WithMessage("ZipCode must be between 4 and 10 characters.");

            RuleFor(c => c.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("State is required.");
        }

        // Método para validar si el cliente tiene al menos 18 años
        private static bool BeAValidAge(DateOnly birthDay)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - birthDay.Year;
            if (birthDay > today.AddYears(-age)) age--; // Si el cumpleaños aún no ha ocurrido este año
            return age >= 18;
        }
    }
}
