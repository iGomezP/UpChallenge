using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Event;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.CompleteTransaction
{
    internal sealed class CompleteTransactionDomainEventHandler : INotificationHandler<TransactionCompletedDomainEvent>
    {
        private readonly ITransactionSqlRepository _transactionSqlRepository;
        private readonly ITransactionMongoRepository _transactionMongoRepository;
        private readonly IEmailService _emailService;

        public CompleteTransactionDomainEventHandler(
            ITransactionSqlRepository transactionSqlRepository,
            ITransactionMongoRepository transactionMongoRepository,
            IEmailService emailService)
        {
            _transactionSqlRepository = transactionSqlRepository;
            _transactionMongoRepository = transactionMongoRepository;
            _emailService = emailService;
        }

        public async Task Handle(TransactionCompletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSqlRepository.GetTransactionByIdAsync(notification.AccountId, cancellationToken);

            if (transaction == null)
            {
                return;
            }

            await _transactionMongoRepository.UpdateStatusAsync(transaction.Id, TransactionStatusEnum.Completed, cancellationToken);

            // Enviar correo electrónico o realizar alguna acción.
            await _emailService.SendMailAsync(
                transaction.Account.Customer.Email,
                "Transaction Completed",
                $"Your transaction with reference {transaction.MovReference} has been completed successfully."
            );
        }
    }
}
