using MongoDB.Driver;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Customers.Repositories;

namespace UpBack.Infrastructure.Repositories
{
    internal sealed class CustomerMongoRepository : ICustomerMongoRepository
    {
        private readonly IMongoCollection<CustomerDto> _customersCollection;

        public CustomerMongoRepository(IMongoDatabase database)
        {
            _customersCollection = database.GetCollection<CustomerDto>("Customers");
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _customersCollection.Find(Builders<CustomerDto>.Filter.Eq(c => c.ObjectStatus, "active")).ToListAsync(cancellationToken);
        }

        public async Task<CustomerDto?> GetByIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<CustomerDto>.Filter.And(
                Builders<CustomerDto>.Filter.Eq(c => c.Id, customerId),
                Builders<CustomerDto>.Filter.Eq(c => c.ObjectStatus, "active")
                );
            return await _customersCollection
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(CustomerDto customer, CancellationToken cancellationToken)
        {
            await _customersCollection.InsertOneAsync(customer, null, cancellationToken);
        }

        public async Task DeleteAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var filter = Builders<CustomerDto>.Filter.Eq(c => c.Id, customerId);
            var update = Builders<CustomerDto>.Update
                .Set(c => c.ObjectStatus, "inactive");
            await _customersCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(CustomerDto customer, CancellationToken cancellationToken)
        {
            var filter = Builders<CustomerDto>.Filter.And(
                Builders<CustomerDto>.Filter.Eq(c => c.Id, customer.Id),
                Builders<CustomerDto>.Filter.Eq(c => c.ObjectStatus, "active")
                );

            var update = Builders<CustomerDto>.Update
                .Set(c => c.PhoneNumber, customer.PhoneNumber)
                .Set(c => c.Address, customer.Address);

            await _customersCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }

        public async Task<CustomerDto?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var filter = Builders<CustomerDto>.Filter.And(
                Builders<CustomerDto>.Filter.Eq(c => c.Email, email),
                Builders<CustomerDto>.Filter.Eq(c => c.ObjectStatus, "active")
                );

            return await _customersCollection
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
