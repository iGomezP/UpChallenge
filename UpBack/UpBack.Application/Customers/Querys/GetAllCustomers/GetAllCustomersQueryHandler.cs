using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Customers;
using UpBack.Domain.Customers.Repositories;

namespace UpBack.Application.Customers.Querys.GetAllCustomers
{
    internal sealed class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
    {
        private readonly ICustomerMongoRepository _customerReadRepository;

        public GetAllCustomersQueryHandler(ICustomerMongoRepository customerReadRepository)
        {
            _customerReadRepository = customerReadRepository;
        }

        public async Task<Result<IEnumerable<CustomerDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerReadRepository.GetAllAsync(cancellationToken);

            if (customers == null || !customers.Any())
            {
                return Result.Failure<IEnumerable<CustomerDto>>(CustomerErrors.NotFound);
            }

            return Result.Success(customers);
        }
    }
}
