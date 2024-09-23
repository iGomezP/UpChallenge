using UpBack.Domain.Abstractions;
using UpBack.Domain.Accounts;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Transactions.Event;

namespace UpBack.Domain.Transactions
{
    public sealed class Transaction : Entity
    {
        private Transaction()
        {

        }
        private Transaction(
            Guid id,
            TransactionType type,
            Guid accountId,
            TransactionQuantity quantity,
            DateTime transactionDate,
            Account account,
            string reference,
            TransactionStatusEnum status
            ) : base(id)
        {
            if (account.Balance.Value <= 0)
            {
                throw new InvalidOperationException("Cannot perform transaction: Account balance is zero.");
            }

            Type = type;
            AccountId = accountId;
            Quantity = quantity;
            TransactionDate = transactionDate;
            Account = account;
            MovReference = reference;
            Status = status;
        }

        public TransactionType Type { get; private set; }
        public Guid AccountId { get; private set; }
        public TransactionQuantity Quantity { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public Account Account { get; private set; }
        public string MovReference { get; private set; } = "System-Transaction";
        public TransactionStatusEnum Status { get; private set; }

        public static Transaction Create(
            TransactionType type,
            Guid accountId,
            TransactionQuantity quantity,
            DateTime transactionDate,
            Account account,
            string reference,
            TransactionStatusEnum status
            )
        {
            var transaction = new Transaction(Guid.NewGuid(), type, accountId, quantity, transactionDate, account, reference, status);
            transaction.RaiseDomainEvent(new TransactionCreatedDomainEvent(transaction.Id));
            return transaction;
        }

        public Result UpdateDetails(TransactionQuantity newQuantity, string newReference, TransactionStatusEnum newStatus)
        {
            Quantity = newQuantity;
            MovReference = newReference;
            Status = newStatus;

            RaiseDomainEvent(new TransactionUpdatedDomainEvent(Id, AccountId));

            return Result.Success();
        }

        public Result Complete()
        {
            if (Status != TransactionStatusEnum.Pending)
            {
                return Result.Failure(TransactionErrors.NotCompleted);
            }

            Status = TransactionStatusEnum.Completed;
            RaiseDomainEvent(new TransactionCompletedDomainEvent(AccountId));
            return Result.Success();
        }

        public Result Fail()
        {
            if (Status != TransactionStatusEnum.Pending)
            {
                return Result.Failure(TransactionErrors.NotFailed);
            }

            Status = TransactionStatusEnum.Failed;
            RaiseDomainEvent(new TransactionFailedDomainEvent(AccountId));
            return Result.Success();
        }

        public Result Cancel()
        {
            if (Status != TransactionStatusEnum.Pending)
            {

                return Result.Failure(TransactionErrors.NotCancelled);
            }

            Status = TransactionStatusEnum.Cancelled;
            RaiseDomainEvent(new TransactionCancelledDomainEvent(AccountId));
            return Result.Success();
        }

        public Result Reject()
        {
            if (Status != TransactionStatusEnum.Pending)
            {
                return Result.Failure(TransactionErrors.NotRejected);
            }
            Status = TransactionStatusEnum.Rejected;
            RaiseDomainEvent(new TransactionRejectedDomainEvent(Id));
            return Result.Success();
        }

        public Result Refund()
        {
            if (Status != TransactionStatusEnum.Completed)
            {
                return Result.Failure(TransactionErrors.NotRefunded);
            }

            Status = TransactionStatusEnum.Refunded;
            RaiseDomainEvent(new TransactionRefundedDomainEvent(AccountId));
            return Result.Success();
        }
    }
}
