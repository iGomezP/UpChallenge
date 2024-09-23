using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Domain.Customers.Events;
using UpBack.Domain.Customers.Repositories;

namespace UpBack.Application.Customers.Commands.DeleteCustomer
{
    internal sealed class DeleteCustomerDomainEventHandler : INotificationHandler<CustomerDeletedDomainEvent>
    {
        private readonly ICustomerSqlRepository _customerSqlRepository;
        private readonly ICustomerMongoRepository _customerMongoRepository;
        private readonly IEmailService _emailService;

        public DeleteCustomerDomainEventHandler(
            ICustomerSqlRepository customerSqlRepository,
            ICustomerMongoRepository customerMongoRepository,
            IEmailService emailService)
        {
            _customerSqlRepository = customerSqlRepository;
            _customerMongoRepository = customerMongoRepository;
            _emailService = emailService;
        }

        public async Task Handle(CustomerDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var customer = await _customerSqlRepository.GetByIdAsync(notification.CustomerId, cancellationToken);

            if (customer is null || customer.Email is null)
            {
                return;
            }

            await _customerMongoRepository.DeleteAsync(customer.Id, cancellationToken);

            await _emailService.SendMailAsync(
                customer.Email,
                "Account Deletion Confirmation",
                $"Dear {customer.Name},\nYour account has been successfully deleted. If you have any questions, feel free to contact us.\nBest regards,\nThe UpBack Team"
            );
        }
    }
}
