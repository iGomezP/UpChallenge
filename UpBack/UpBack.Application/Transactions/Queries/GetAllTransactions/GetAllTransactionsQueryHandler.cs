using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Queries.GetAllTransactions
{
    public sealed class GetAllTransactionsQueryHandler : IQueryHandler<GetAllTransactionsQuery, IEnumerable<TransactionDto>>
    {
        private readonly ITransactionMongoRepository _transactionReadRepository;

        public GetAllTransactionsQueryHandler(ITransactionMongoRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }

        public async Task<Result<IEnumerable<TransactionDto>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionReadRepository.GetAllAsync(cancellationToken);

            if (transactions == null || !transactions.Any())
            {
                return Result.Failure<IEnumerable<TransactionDto>>(TransactionErrors.NotFound);
            }

            return Result.Success(transactions);
        }
    }
}
