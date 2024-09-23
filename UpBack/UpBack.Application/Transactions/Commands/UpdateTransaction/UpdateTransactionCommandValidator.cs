using FluentValidation;

namespace UpBack.Application.Transactions.Commands.UpdateTransaction
{
    public sealed class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public UpdateTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionId).NotEmpty();
            RuleFor(x => x.NewQuantity).GreaterThan(0);
            RuleFor(x => x.NewReference).NotEmpty().MaximumLength(30);
            RuleFor(x => x.NewStatus).IsInEnum();
        }
    }
}
