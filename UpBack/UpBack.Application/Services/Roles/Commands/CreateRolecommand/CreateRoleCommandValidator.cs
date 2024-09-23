using FluentValidation;

namespace UpBack.Application.Services.Roles.Commands.CreateRolecommand
{
    public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("The Title is required.")
                .MaximumLength(50).WithMessage("The Title must be at most 50 characters long.");

            RuleFor(x => x.Permissions)
                .NotEmpty().WithMessage("The Role must have at least one Permission.");

            RuleFor(x => x.ObjectStatus)
                .Must(x => x == "active" || x == "inactive")
                .WithMessage("The ObjectStatus must be either 'active' or 'inactive'.");
        }
    }
}
