using FluentValidation;

namespace UpBack.Application.Customers.Commands.DeleteCustomer
{
    public sealed class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}
