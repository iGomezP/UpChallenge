using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Roles;
using UpBack.Domain.Roles.Repositories;

namespace UpBack.Application.Services.Roles.Querys.GetById
{
    internal sealed class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRoleMongoRepository _roleMongoRepository;

        public GetRoleByIdQueryHandler(IRoleMongoRepository roleMongoRepository)
        {
            _roleMongoRepository = roleMongoRepository;
        }

        public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleMongoRepository.GetByIdAsync(request.RoleId, cancellationToken);

            if (role == null)
            {
                return Result.Failure<RoleDto>(RoleErrors.NotFound);
            }

            return Result.Success(role);
        }
    }
}
