using MongoDB.Driver;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Accounts.Repositories;

namespace UpBack.Infrastructure.Repositories
{
    internal sealed class AccountMongoRepository(IMongoDatabase mongoDatabase) : IAccountMongoRepository
    {
        private readonly IMongoCollection<AccountDto> _accountsCollection = mongoDatabase.GetCollection<AccountDto>("Accounts");
        private readonly FilterDefinitionBuilder<AccountDto> _filterDefinitionBuilder = Builders<AccountDto>.Filter;

        public async Task<IEnumerable<AccountDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _accountsCollection.Find(_filterDefinitionBuilder.Empty).ToListAsync(cancellationToken);
        }

        public async Task<AccountDto> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            var filter = _filterDefinitionBuilder.Eq(acc => acc.Customer.Id, customerId);
            return await _accountsCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<AccountDto?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var filter = _filterDefinitionBuilder.Eq(acc => acc.Id, accountId);
            return await _accountsCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(AccountDto account, CancellationToken cancellationToken = default)
        {
            await _accountsCollection.InsertOneAsync(account, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            await _accountsCollection.DeleteOneAsync(account => account.Id == accountId, cancellationToken);
        }

        public async Task UpdateAsync(AccountDto account, CancellationToken cancellationToken = default)
        {
            await _accountsCollection.ReplaceOneAsync(a => a.Id == account.Id, account, cancellationToken: cancellationToken);
        }

        public async Task<AccountDto?> GetByNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
        {
            var filter = _filterDefinitionBuilder.And(
                _filterDefinitionBuilder.Eq(acc => acc.AccountNumber, accountNumber),
                _filterDefinitionBuilder.Eq(acc => acc.ObjectStatus, "active")
                );
            return await _accountsCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
