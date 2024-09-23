using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Domain.Roles.Repositories
{
    public interface IRoleMongoRepository
    {
        Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RoleDto> GetByTitleAsync(string title);
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task UpdateAsync(RoleDto role);
        Task DeleteAsync(Guid id);
        Task AddAsync(RoleDto role, CancellationToken cancellationToken);
    }
}
