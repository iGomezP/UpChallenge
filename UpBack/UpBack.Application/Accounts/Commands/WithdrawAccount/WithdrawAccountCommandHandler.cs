using MediatR;
using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Transactions.Commands.CreateTransaction;
using UpBack.Application.Transactions.Commands.RejectTransaction;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.ObjectValues;

namespace UpBack.Application.Accounts.Commands.WithdrawAccount
{
    internal sealed class WithdrawAccountCommandHandler : ICommandHandler<WithdrawAccountCommand, Guid>
    {
        private readonly IAccountSqlRepository _accountWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public WithdrawAccountCommandHandler(IAccountSqlRepository accountWriteRepository, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _accountWriteRepository = accountWriteRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(WithdrawAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountWriteRepository.GetByIdAsync(request.AccountId, cancellationToken);
            if (account == null)
            {
                return Result.Failure<Guid>(AccountErrors.NotFound);
            }

            var newReference = request.Reference;

            var withdrawResult = account.Withdraw(AccountBalance.Create(request.Amount).Value, newReference);
            if (withdrawResult.IsFailure)
            {
                var command = new CreateTransactionCommand(
                    account.Id,
                    TransactionType.Create("Withdrawal"),
                    request.Amount,
                    DateTime.UtcNow,
                    request.Reference,
                    Domain.Transactions.TransactionStatusEnum.Pending
                    );

                var rejectedTransaction = await _mediator.Send(command, cancellationToken);

                if (rejectedTransaction.IsFailure)
                {
                    return Result.Failure<Guid>(withdrawResult.Error);
                }

                var rejectedCommand = new RejectTransactionCommand(rejectedTransaction.Value);
                var result = await _mediator.Send(rejectedCommand, cancellationToken);

                if (result.IsFailure)
                {
                    return Result.Failure<Guid>(withdrawResult.Error);

                }

                return Result.Failure<Guid>(withdrawResult.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(account.Id);
        }
    }
}
