using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Domain.Accounts.Events;
using UpBack.Domain.Accounts.Repositories;

namespace UpBack.Application.Accounts.Commands.DeleteAccount
{
    internal sealed class DeleteAccountDomainEventHandler : INotificationHandler<AccountDeletedDomainEvent>
    {
        private readonly IAccountSqlRepository _accountSqlRepository;
        private readonly IAccountMongoRepository _accountMongoRepository;
        private readonly IEmailService _emailService;

        public DeleteAccountDomainEventHandler(
            IAccountSqlRepository accountSqlRepository,
            IAccountMongoRepository accountMongoRepository,
            IEmailService emailService)
        {
            _accountSqlRepository = accountSqlRepository;
            _accountMongoRepository = accountMongoRepository;
            _emailService = emailService;
        }

        public async Task Handle(AccountDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var account = await _accountSqlRepository.GetAccountByIdAsync(notification.AccountId, cancellationToken);

            if (account is null || account.Customer.Email is null)
            {
                return;
            }

            await _accountMongoRepository.DeleteAsync(notification.AccountId, cancellationToken);

            // Enviar correo de notificación
            await _emailService.SendMailAsync(
                account.Customer.Email,
                "Account Deleted",
                $"Hi {account.Customer.Name}!\nYour account with number {account.Number.Value} has been deleted successfully." +
                $"\n\nBest regards,\nThe UpBack Team"
            );
        }
    }
}
