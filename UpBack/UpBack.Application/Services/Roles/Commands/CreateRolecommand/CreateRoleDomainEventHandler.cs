using MediatR;
using UpBack.Application.Mappers;
using UpBack.Domain.Roles.Events;
using UpBack.Domain.Roles.Repositories;

namespace UpBack.Application.Services.Roles.Commands.CreateRolecommand
{
    internal sealed class CreateRoleDomainEventHandler : INotificationHandler<RoleCreatedDomainEvent>
    {
        private readonly IRoleSqlRepository _roleRepository;
        private readonly IRoleMongoRepository _roleMongoRepository;

        public CreateRoleDomainEventHandler(IRoleSqlRepository roleRepository, IRoleMongoRepository roleMongoRepository)
        {
            _roleRepository = roleRepository;
            _roleMongoRepository = roleMongoRepository;
        }

        public async Task Handle(RoleCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(notification.RoleId, cancellationToken);

            if (role == null)
            {
                return; // Handle error if needed
            }

            await _roleMongoRepository.AddAsync(role.MapToMongoDto(), cancellationToken);

            // Aquí puedes enviar correos, loggear o realizar alguna acción con el Role creado
        }
    }
}
