using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Customers;
using UpBack.Domain.Customers.Repositories;
using UpBack.Domain.ObjectValues;

namespace UpBack.Application.Customers.Commands.UpdateCustomer
{
    internal sealed class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, Guid>
    {
        private readonly ICustomerSqlRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerSqlRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
            if (customer == null)
            {
                return Result.Failure<Guid>(CustomerErrors.NotFound);
            }

            var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
            var address = Address.Create(request.Street, request.City, request.ZipCode, request.Country, request.State);

            try
            {
                customer.UpdateContactInfo(address, phoneNumber);

                _customerRepository.Update(customer);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success(customer.Id);
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(CustomerErrors.DuplicateEmail);
            }
        }
    }
}
