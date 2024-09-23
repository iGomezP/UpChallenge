using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Application.Mappers;
using UpBack.Domain.Customers.Events;
using UpBack.Domain.Customers.Repositories;

namespace UpBack.Application.Customers.Commands.UpdateCustomer
{
    internal sealed class UpdateCustomerDomainEventHandler : INotificationHandler<CustomerContactInfoUpdatedDomainEvent>
    {
        private readonly ICustomerSqlRepository _customerSqlRepository;
        private readonly ICustomerMongoRepository _customerMongoRepository;
        private readonly IEmailService _emailService;

        public UpdateCustomerDomainEventHandler(
            ICustomerSqlRepository customerSqlRepository,
            ICustomerMongoRepository customerMongoRepository,
            IEmailService emailService)
        {
            _customerSqlRepository = customerSqlRepository;
            _customerMongoRepository = customerMongoRepository;
            _emailService = emailService;
        }

        public async Task Handle(CustomerContactInfoUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // Obtener el cliente actualizado
            var customer = await _customerSqlRepository.GetByIdAsync(notification.CustomerId, cancellationToken);

            if (customer is null || customer.Email is null)
            {
                return;
            }

            await _customerMongoRepository.UpdateAsync(customer.MapToMongoDto(), cancellationToken);

            // Enviar correo de confirmación de actualización
            await _emailService.SendMailAsync(
                customer.Email,
                "Account Information Updated",
                $"Dear {customer.Name},\nYour account information has been successfully updated. If you did not request this change, please contact us immediately.\n\nBest regards,\nThe UpBack Team"
            );
        }
    }
}
