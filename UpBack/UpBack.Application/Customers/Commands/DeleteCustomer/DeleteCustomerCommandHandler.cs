using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Customers;
using UpBack.Domain.Customers.Repositories;

namespace UpBack.Application.Customers.Commands.DeleteCustomer
{
    internal sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, Guid>
    {
        private readonly ICustomerSqlRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(ICustomerSqlRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
            if (customer == null)
            {
                return Result.Failure<Guid>(CustomerErrors.NotFound);
            }

            try
            {
                customer.Delete();
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
