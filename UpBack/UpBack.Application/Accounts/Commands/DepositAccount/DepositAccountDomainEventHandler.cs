using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Application.Mappers;
using UpBack.Application.Transactions.Commands.CreateTransaction;
using UpBack.Domain.Accounts.Events;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Transactions;

namespace UpBack.Application.Accounts.Commands.DepositAccount
{
    internal sealed class DepositAccountDomainEventHandler : INotificationHandler<FundsDepositedDomainEvent>
    {
        private readonly IAccountSqlRepository _accountSqlRepository;
        private readonly IAccountMongoRepository _accountMongoRepository;
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;

        public DepositAccountDomainEventHandler(
            IAccountSqlRepository accountSqlRepository,
            IAccountMongoRepository accountMongoRepository,
            IEmailService emailService,
            IMediator mediator)
        {
            _accountSqlRepository = accountSqlRepository;
            _accountMongoRepository = accountMongoRepository;
            _emailService = emailService;
            _mediator = mediator;
        }

        public async Task Handle(FundsDepositedDomainEvent notification, CancellationToken cancellationToken)
        {
            // Obtener la cuenta actualizada por su Id
            var account = await _accountSqlRepository.GetAccountByIdAsync(notification.AccountId, cancellationToken);

            if (account == null)
            {
                return;
            }

            var command = new CreateTransactionCommand
            (
                notification.AccountId,
                TransactionType.Create("Deposit"),
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

            await _accountMongoRepository.UpdateAsync(account.MapToMongoDto(), cancellationToken);

            // Aquí puedes realizar cualquier acción, como enviar una notificación
            await _emailService.SendMailAsync(
                account.Customer.Email,
                "Account Updated",
                $"Hi {account.Customer.Name}!\nYour account with number {account.Number.Value} has been updated." +
                $"\nNew balance: {account.Balance.Value:C}, Status: {account.ObjectStatus}." +
                $"\n\nBest regards,\nThe UpBack Team"
            );
        }
    }
}
