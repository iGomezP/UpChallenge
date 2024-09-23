using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Application.Mappers;
using UpBack.Domain.Transactions.Event;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.RefundTransaction
{
    internal sealed class RefundTransactionDomainEventHandler : INotificationHandler<TransactionRefundedDomainEvent>
    {
        private readonly ITransactionSqlRepository _transactionSqlRepository;
        private readonly ITransactionMongoRepository _transactionMongoRepository;
        private readonly IEmailService _emailService;

        public RefundTransactionDomainEventHandler(
            ITransactionSqlRepository transactionSqlRepository,
            ITransactionMongoRepository transactionMongoRepository,
            IEmailService emailService)
        {
            _transactionSqlRepository = transactionSqlRepository;
            _transactionMongoRepository = transactionMongoRepository;
            _emailService = emailService;
        }

        public async Task Handle(TransactionRefundedDomainEvent notification, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSqlRepository.GetTransactionByIdAsync(notification.AccountId, cancellationToken);

            if (transaction == null)
            {
                return;
            }

            await _transactionMongoRepository.UpdateAsync(transaction.MapToMongoDto(), cancellationToken);

            // Enviar correo electrónico o realizar alguna acción.
            await _emailService.SendMailAsync(
                transaction.Account.Customer.Email,
                "Transaction Refunded",
                $"Your transaction with reference {transaction.MovReference} has been refunded."
            );
        }
    }
}
