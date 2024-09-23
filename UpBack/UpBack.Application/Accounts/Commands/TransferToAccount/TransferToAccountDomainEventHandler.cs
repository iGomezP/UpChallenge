using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Application.Mappers;
using UpBack.Application.Transactions.Commands.CreateTransaction;
using UpBack.Domain.Accounts.Events;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Transactions;

namespace UpBack.Application.Accounts.Commands.TransferToAccount
{
    internal sealed class TransferToAccountDomainEventHandler : INotificationHandler<FundsTransferredDomainEvent>
    {
        private readonly IAccountSqlRepository _accountRepository;
        private readonly IAccountMongoRepository _accountMongoRepository;
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;

        public TransferToAccountDomainEventHandler(
            IAccountSqlRepository accountRepository,
            IAccountMongoRepository accountMongoRepository,
            IEmailService emailService,
            IMediator mediator)
        {
            _accountRepository = accountRepository;
            _accountMongoRepository = accountMongoRepository;
            _emailService = emailService;
            _mediator = mediator;
        }

        public async Task Handle(FundsTransferredDomainEvent notification, CancellationToken cancellationToken)
        {
            // Obtener las cuentas desde SQL
            var sourceAccount = await _accountRepository.GetAccountByIdAsync(notification.SourceAccountId, cancellationToken);
            var targetAccount = await _accountRepository.GetAccountByIdAsync(notification.TargetAccountId, cancellationToken);

            if (sourceAccount is null || targetAccount is null)
            {
                return; // Si alguna cuenta no existe, no se hace nada.
            }

            // Actualizar MongoDB con las nuevas informaciones
            await _accountMongoRepository.UpdateAsync(sourceAccount.MapToMongoDto(), cancellationToken);
            await _accountMongoRepository.UpdateAsync(targetAccount.MapToMongoDto(), cancellationToken);

            var command = new CreateTransactionCommand
            (
                sourceAccount.Id,
                TransactionType.Create("Transfer"),
                notification.Amount,
                DateTime.UtcNow,
                notification.Reference,
                TransactionStatusEnum.Completed
            );

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return;
            }

            // Enviar email de notificación al titular de la cuenta origen
            if (sourceAccount.Customer.Email is not null)
            {
                await _emailService.SendMailAsync(
                    sourceAccount.Customer.Email,
                    "Funds Transferred",
                    $"Hi {sourceAccount.Customer.Name}!\nYou have successfully transferred {notification.Amount:C} from your account ({sourceAccount.Number.Value}) to account ({targetAccount.Number.Value})." +
                    $"\n\nBest regards,\nThe UpBack Team"
                );
            }

            // Enviar email de notificación al titular de la cuenta destino
            if (targetAccount.Customer.Email is not null)
            {
                await _emailService.SendMailAsync(
                    targetAccount.Customer.Email,
                    "Funds Received",
                    $"Hi {targetAccount.Customer.Name}!\nYou have received {notification.Amount:C} into your account ({targetAccount.Number.Value}) from account ({sourceAccount.Number.Value})." +
                    $"\n\nBest regards,\nThe UpBack Team"
                );
            }
        }
    }
}
