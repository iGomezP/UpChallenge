using FluentValidation;

namespace UpBack.Application.Customers.Commands.UpdateCustomer
{
    public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty().Length(7, 15);
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.State).NotEmpty();
        }

        private bool BeAtLeast18YearsOld(DateOnly birthDay)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - birthDay.Year;
            if (birthDay > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
