using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Queries.GetTransactionsByAccount
{
    public sealed class GetTransactionsByAccountQueryHandler : IQueryHandler<GetTransactionsByAccountQuery, IEnumerable<TransactionDto>>
    {
        private readonly ITransactionMongoRepository _transactionReadRepository;

        public GetTransactionsByAccountQueryHandler(ITransactionMongoRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }

        public async Task<Result<IEnumerable<TransactionDto>>> Handle(GetTransactionsByAccountQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionReadRepository.GetByAccountIdAsync(request.AccountId, cancellationToken);

            if (transactions == null || !transactions.Any())
            {
                return Result.Failure<IEnumerable<TransactionDto>>(TransactionErrors.NotFound);
            }

            var response = transactions.Select(transaction => transaction);
            return Result.Success(response);
        }
    }
}
