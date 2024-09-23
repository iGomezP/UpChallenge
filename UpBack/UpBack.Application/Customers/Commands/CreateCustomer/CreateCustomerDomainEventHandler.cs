using MediatR;
using UpBack.Application.Abstractions.Email;
using UpBack.Application.Accounts.Commands.CreateAccount;
using UpBack.Application.Mappers;
using UpBack.Domain.Customers.Events;
using UpBack.Domain.Customers.Repositories;

namespace UpBack.Application.Customers.Commands.CreateCustomer
{
    internal sealed class CreateCustomerDomainEventHandler : INotificationHandler<CustomerCreatedDomainEvent>
    {
        private readonly ICustomerSqlRepository _customerSqlRepository;
        private readonly ICustomerMongoRepository _customerMongoRepository;
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;

        public CreateCustomerDomainEventHandler(
            ICustomerSqlRepository customerSqlRepository,
            ICustomerMongoRepository customerMongoRepository,
            IEmailService emailService,
            IMediator mediator)
        {
            _customerSqlRepository = customerSqlRepository;
            _customerMongoRepository = customerMongoRepository;
            _emailService = emailService;
            _mediator = mediator;
        }

        public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var customer = await _customerSqlRepository.GetByIdAsync(notification.CustomerId, cancellationToken);

            if (customer is null || customer.Email is null)
            {
                return;
            }

            var accountCommand = new CreateAccountCommand
            (
                customer.Id,
                0,
                "active"
            );

            var accountResult = await _mediator.Send(accountCommand, cancellationToken);

            if (accountResult.IsFailure)
            {
                return;
            }

            await _customerMongoRepository.AddAsync(customer.MapToMongoDto(), cancellationToken);

            await _emailService.SendMailAsync(
                customer.Email,
                "Welcome",
                $"Hi {customer.Name}!\nWelcome to UpBack! We're thrilled to have you on board as our newest customer." +
                $"\nIf you have any questions, feel free to reach out—your financial journey starts here!" +
                $"\n\nBest regards,\nThe UpBack Team"
                );
        }
    }
}
