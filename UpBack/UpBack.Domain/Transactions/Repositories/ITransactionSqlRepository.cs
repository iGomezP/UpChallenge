namespace UpBack.Domain.Transactions.Repositories
{
    public interface ITransactionSqlRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(Transaction transaction);
        Task<Transaction?> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
