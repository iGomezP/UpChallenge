using UpBack.Domain.Abstractions;
using UpBack.Domain.Accounts.Events;
using UpBack.Domain.Customers;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Transactions.Event;

namespace UpBack.Domain.Accounts
{
    public sealed class Account : Entity
    {
        private Account()
        {

        }
        private Account(
            Guid id,
            AccountNumber number,
            Guid customerId,
            AccountBalance balance,
            string reference,
            Customer customer,
            DateTime createdDate,
            string objectStatus
            ) : base(id)
        {
            Id = id;
            Number = number;
            CustomerId = customerId;
            Balance = balance;
            MovReference = reference;
            Customer = customer;
            CreatedDate = createdDate;
            ObjectStatus = objectStatus;
        }

        public AccountNumber Number { get; private set; }
        public Guid CustomerId { get; private set; }
        public AccountBalance Balance { get; private set; }
        public string MovReference { get; private set; } = "System-Account";
        public Customer Customer { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string ObjectStatus { get; private set; } = "active";

        public static Account Create(
            Guid customerId,
            AccountBalance balance,
            string reference,
            Customer customer,
            DateTime createdDate,
            string objectStatus
            )
        {
            var newAccountNumber = GenerateAccountNumber();
            var account = new Account(Guid.NewGuid(), newAccountNumber, customerId, balance, reference, customer, createdDate, objectStatus);
            account.RaiseDomainEvent(new AccountCreatedDomainEvent(account.Id, account.CustomerId));
            return account;
        }

        public static Account Update(
            Guid id,
            AccountNumber number,
            Guid customerId,
            AccountBalance balance,
            string reference,
            Customer customer,
            DateTime createdDate,
            string objectStatus
            )
        {
            if (objectStatus != "active" && objectStatus != "inactive")
            {
                throw new ArgumentException("ObjectStatus must be either 'active' or 'inactive'.", nameof(objectStatus));
            }

            var account = new Account(id, number, customerId, balance, reference, customer, createdDate, objectStatus);

            account.RaiseDomainEvent(new AccountUpdatedDomainEvent(account.Id, account.Balance.Value, account.ObjectStatus));

            return account;
        }

        public Result Withdraw(AccountBalance amount, string reference)
        {
            if (Balance.Value < amount.Value)
            {
                RaiseDomainEvent(new TransactionRejectedDomainEvent(Id));
                return Result.Failure(AccountErrors.InsufficientBalance);
            }

            var newBalanceResult = AccountBalance.Create(Balance.Value - amount.Value);
            if (newBalanceResult.IsFailure)
            {
                return newBalanceResult;
            }

            Balance = newBalanceResult.Value;
            RaiseDomainEvent(new FundsWithdrawnDomainEvent(Id, amount.Value, reference));
            return Result.Success();
        }

        public Result Deposit(AccountBalance amount, string reference)
        {
            if (amount.Value <= 0)
            {
                return Result.Failure(AccountErrors.AmountZero);
            }

            var newBalanceResult = AccountBalance.Create(Balance.Value + amount.Value);
            if (newBalanceResult.IsFailure)
            {
                return newBalanceResult;
            }

            Balance = newBalanceResult.Value;
            MovReference = reference;
            RaiseDomainEvent(new FundsDepositedDomainEvent(Id, amount.Value, reference));
            return Result.Success();
        }

        public Result CloseAccount()
        {
            if (Balance.Value != 0)
            {
                return Result.Failure(AccountErrors.BalanceNotZero);
            }

            ObjectStatus = "inactive";
            RaiseDomainEvent(new AccountDeletedDomainEvent(Id));
            return Result.Success();
        }

        public Result TransferTo(Account targetAccount, AccountBalance amount, string reference)
        {
            // Verificar si hay suficiente balance en la cuenta origen
            if (Balance.Value < amount.Value)
            {
                return Result.Failure(AccountErrors.InsufficientBalance);
            }

            // Crear un nuevo balance para la cuenta origen
            var newBalanceResult = AccountBalance.Create(Balance.Value - amount.Value);
            if (newBalanceResult.IsFailure)
            {
                return newBalanceResult;
            }

            // Actualizar el balance de la cuenta origen
            Balance = newBalanceResult.Value;
            MovReference = reference;

            // Realizar el depósito en la cuenta destino
            var depositResult = targetAccount.Deposit(amount, reference);
            if (depositResult.IsFailure)
            {
                return depositResult;
            }

            RaiseDomainEvent(new FundsTransferredDomainEvent(Id, targetAccount.Id, amount.Value, reference));

            return Result.Success();
        }

        private static AccountNumber GenerateAccountNumber()
        {
            var random = new Random();
            var accountNumber = new string(Enumerable.Repeat("0123456789", 20)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return AccountNumber.Create(accountNumber);
        }
    }
}
