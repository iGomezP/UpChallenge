using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Queries.GetTransactionById
{
    public sealed class GetTransactionByIdQueryHandler : IQueryHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly ITransactionMongoRepository _transactionReadRepository;

        public GetTransactionByIdQueryHandler(ITransactionMongoRepository transactionReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
        }

        public async Task<Result<TransactionDto>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionReadRepository.GetByIdAsync(request.TransactionId, cancellationToken);

            if (transaction == null)
            {
                return Result.Failure<TransactionDto>(TransactionErrors.NotFound);
            }

            return Result.Success(transaction);
        }
    }
}
