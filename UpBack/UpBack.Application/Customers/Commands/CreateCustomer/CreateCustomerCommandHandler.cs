using UpBack.Application.Abstractions.Clock;
using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Customers;
using UpBack.Domain.Customers.Repositories;
using UpBack.Domain.ObjectValues;

namespace UpBack.Application.Customers.Commands.CreateCustomer
{
    internal sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerSqlRepository _customerRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, ICustomerSqlRepository customerRepository, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (await _customerRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                return Result.Failure<Guid>(CustomerErrors.DuplicateEmail);
            }

            var name = Name.Create(request.Name);
            var password = Password.Create(request.Password);
            var lastName = LastName.Create(request.LastName);
            var email = CustomerEmail.Create(request.Email);
            var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
            var address = Address.Create(request.Street, request.City, request.ZipCode, request.Country, request.State);

            try
            {
                var customer = Customer.Create(
                    name,
                    lastName,
                    email,
                    phoneNumber,
                    request.BirthDay,
                    address,
                    password.Value,
                    _dateTimeProvider.CurrentTime,
                    "active"
                    );

                _customerRepository.Add(customer);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return customer.Id;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(CustomerErrors.GeneralFailure);
            }
        }
    }
}
