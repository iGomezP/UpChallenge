using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Application.Mappers;
using UpBack.Application.Transactions.Commands.CreateTransaction;
using UpBack.Domain.Accounts.Events;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.ObjectValues;

namespace UpBack.Application.Accounts.Commands.WithdrawAccount
{
    internal sealed class WithdrawAccountDomainEventHandler : INotificationHandler<FundsWithdrawnDomainEvent>
    {
        private readonly IAccountSqlRepository _accountSqlRepository;
        private readonly IAccountMongoRepository _accountMongoRepository;
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;

        public WithdrawAccountDomainEventHandler(
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

        public async Task Handle(FundsWithdrawnDomainEvent notification, CancellationToken cancellationToken)
        {
            var account = await _accountSqlRepository.GetAccountByIdAsync(notification.AccountId, cancellationToken);
            if (account == null || account.Customer == null)
            {
                return;
            }

            var command = new CreateTransactionCommand(
                    account.Id,
                    TransactionType.Create("Withdrawal"),
                    notification.Amount,
                    DateTime.UtcNow,
                    notification.Reference,
                    Domain.Transactions.TransactionStatusEnum.Completed
                    );

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return;
            }

            await _emailService.SendMailAsync(
                account.Customer.Email,
                "Funds Withdrawn",
                $"You have withdrawn {notification.Amount:C} from your account with number {account.Number.Value}."
            );

            await _accountMongoRepository.UpdateAsync(account.MapToMongoDto(), cancellationToken);
        }
    }
}
