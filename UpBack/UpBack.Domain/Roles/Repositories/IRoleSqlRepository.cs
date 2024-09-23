namespace UpBack.Domain.Roles.Repositories
{
    public interface IRoleSqlRepository
    {
        Task<Role?> GetByIdAsync(Guid roleId, CancellationToken cancellationToken);
        void Add(Role role);
        void Update(Role role);
        void Delete(Role role);
    }
}
