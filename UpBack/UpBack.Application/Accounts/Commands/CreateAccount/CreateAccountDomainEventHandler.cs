using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Application.Mappers;
using UpBack.Domain.Accounts.Events;
using UpBack.Domain.Accounts.Repositories;

namespace UpBack.Application.Accounts.Commands.CreateAccount
{
    internal sealed class CreateAccountDomainEventHandler : INotificationHandler<AccountCreatedDomainEvent>
    {
        private readonly IAccountSqlRepository _accountSqlRepository;
        private readonly IAccountMongoRepository _accountMongoRepository;
        private readonly IEmailService _emailService;

        public CreateAccountDomainEventHandler(
            IAccountSqlRepository accountSqlRepository,
            IAccountMongoRepository accountMongoRepository,
            IEmailService emailService)
        {
            _accountSqlRepository = accountSqlRepository;
            _accountMongoRepository = accountMongoRepository;
            _emailService = emailService;
        }

        public async Task Handle(AccountCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // Obtener la cuenta por su ID
            var account = await _accountSqlRepository.GetAccountByIdAsync(notification.AccountId, cancellationToken);

            if (account is null || account.Customer?.Email is null)
            {
                return; // Si no existe la cuenta o el cliente no tiene email, no se hace nada
            }

            await _accountMongoRepository.AddAsync(account.MapToMongoDto(), cancellationToken);

            // Enviar email de confirmación al cliente
            await _emailService.SendMailAsync(
                account.Customer.Email,
                "Account Created",
                $"Hi {account.Customer.Name},\nYour account has been successfully created with the account number {account.Number}." +
                $"\n\nThank you for choosing UpBack.\n\nBest regards,\nThe UpBack Team"
            );
        }
    }
}
