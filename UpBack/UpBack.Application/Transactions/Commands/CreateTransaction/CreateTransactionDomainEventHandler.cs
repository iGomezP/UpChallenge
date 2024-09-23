using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Application.Mappers;
using UpBack.Domain.Transactions.Event;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.CreateTransaction
{
    internal sealed class CreateTransactionDomainEventHandler : INotificationHandler<TransactionCreatedDomainEvent>
    {
        private readonly ITransactionSqlRepository _transactionSqlRepository;
        private readonly ITransactionMongoRepository _transactionMongoRepository;
        private readonly IEmailService _emailService;

        public CreateTransactionDomainEventHandler(
            ITransactionSqlRepository transactionSqlRepository,
            ITransactionMongoRepository transactionMongoRepository,
            IEmailService emailService)
        {
            _transactionSqlRepository = transactionSqlRepository;
            _transactionMongoRepository = transactionMongoRepository;
            _emailService = emailService;
        }

        public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSqlRepository.GetTransactionByIdAsync(notification.TransactionId, cancellationToken);

            if (transaction == null)
            {
                return;
            }

            await _transactionMongoRepository.AddAsync(transaction.MapToMongoDto(), cancellationToken);

            // Enviar correo electrónico o realizar alguna acción.
            await _emailService.SendMailAsync(
                transaction.Account.Customer.Email,
                "Transaction Created",
                $"Your transaction with reference {transaction.MovReference} has been created successfully."
            );
        }
    }
}
