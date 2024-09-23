using MediatR;
using UpBack.Domain.Abstractions;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Permissions;
using UpBack.Domain.Roles;
using UpBack.Domain.Roles.Repositories;

namespace UpBack.Application.Services.Roles.Commands.CreateRolecommand
{
    internal sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<Guid>>
    {
        private readonly IRoleSqlRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IRoleSqlRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var title = Title.Create(request.Title);

            if (title.IsFailure)
            {
                return Result.Failure<Guid>(title.Error);
            }

            var permissions = request.Permissions
            .Select(p => Permission.Create(
                Title.Create(p.Title.Value).Value,
                Scope.Create(p.Scope.Value).Value,
                DateTime.UtcNow,
                "active"))
            .ToList();

            var role = Role.Create(
                title.Value,
                request.Permissions,
                DateTime.UtcNow,
                request.ObjectStatus
            );

            _roleRepository.Add(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(role.Id);
        }
    }
}
