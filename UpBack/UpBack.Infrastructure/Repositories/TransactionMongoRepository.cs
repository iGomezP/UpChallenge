using MongoDB.Driver;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Infrastructure.Repositories
{
    internal sealed class TransactionMongoRepository : ITransactionMongoRepository
    {
        private readonly IMongoCollection<TransactionDto> _transactionsCollection;
        private readonly FilterDefinitionBuilder<TransactionDto> _filterDefinitionBuilder;

        public TransactionMongoRepository(IMongoDatabase database)
        {
            _transactionsCollection = database.GetCollection<TransactionDto>("Transactions");
            _filterDefinitionBuilder = Builders<TransactionDto>.Filter;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _transactionsCollection.Find(_filterDefinitionBuilder.Empty).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TransactionDto>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var filter = _filterDefinitionBuilder.Eq(t => t.AccountId, accountId);
            return await _transactionsCollection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<TransactionDto?> GetByIdAsync(Guid transactionId, CancellationToken cancellationToken = default)
        {
            var filter = _filterDefinitionBuilder.Eq(t => t.Id, transactionId);
            return await _transactionsCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateStatusAsync(Guid transactionId, TransactionStatusEnum status, CancellationToken cancellationToken)
        {
            var filter = Builders<TransactionDto>.Filter.Eq(t => t.Id, transactionId);
            var update = Builders<TransactionDto>.Update.Set(t => t.Status, status);

            await _transactionsCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }

        public async Task AddAsync(TransactionDto transaction, CancellationToken cancellationToken)
        {
            await _transactionsCollection.InsertOneAsync(transaction, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(TransactionDto transaction, CancellationToken cancellationToken)
        {
            var filter = Builders<TransactionDto>.Filter.Eq(t => t.Id, transaction.Id);
            await _transactionsCollection.ReplaceOneAsync(filter, transaction, cancellationToken: cancellationToken);
        }
    }
}
