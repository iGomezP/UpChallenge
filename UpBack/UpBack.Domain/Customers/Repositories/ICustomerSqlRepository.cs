namespace UpBack.Domain.Customers.Repositories
{
    public interface ICustomerSqlRepository
    {
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);

        // Para persistir el usuario dentro de la aplicación no para insertarlo en la BD
        // Para confirmar la inserción se usa otro componente basado en el partón IUnitOfWork
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
