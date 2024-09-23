using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Customers;
using UpBack.Domain.Customers.Repositories;

namespace UpBack.Application.Customers.Querys.GetCustomerById
{
    internal sealed class GetByEmailAndPassHandler : IQueryHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerMongoRepository _customerReadRepository;

        public GetByEmailAndPassHandler(ICustomerMongoRepository customerReadRepository)
        {
            _customerReadRepository = customerReadRepository;
        }

        public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer == null)
            {
                return Result.Failure<CustomerDto>(CustomerErrors.NotFound);
            }

            return Result.Success(customer);
        }
    }
}
