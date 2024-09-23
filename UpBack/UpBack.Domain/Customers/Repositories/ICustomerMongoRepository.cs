using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Domain.Customers.Repositories
{
    public interface ICustomerMongoRepository
    {
        Task<CustomerDto?> GetByIdAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(CustomerDto customer, CancellationToken cancellationToken);
        Task DeleteAsync(Guid customerId, CancellationToken cancellationToken);
        Task UpdateAsync(CustomerDto customer, CancellationToken cancellationToken);
        Task<CustomerDto?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
