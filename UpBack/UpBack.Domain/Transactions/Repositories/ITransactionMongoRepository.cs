using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Domain.Transactions.Repositories
{
    public interface ITransactionMongoRepository
    {
        Task AddAsync(TransactionDto transaction, CancellationToken cancellationToken);
        Task<IEnumerable<TransactionDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TransactionDto>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<TransactionDto?> GetByIdAsync(Guid transactionId, CancellationToken cancellationToken = default);
        Task UpdateAsync(TransactionDto transaction, CancellationToken cancellationToken);
        Task UpdateStatusAsync(Guid transactionId, TransactionStatusEnum status, CancellationToken cancellationToken);
    }
}
