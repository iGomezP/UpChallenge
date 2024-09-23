using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Customers;
using UpBack.Domain.Customers.Repositories;

namespace UpBack.Application.Customers.Querys.GetByEmailAndPass
{
    internal sealed class GetByEmailAndPassHandler : IQueryHandler<GetByEmailAndPassQuery, CustomerDto>
    {
        private readonly ICustomerMongoRepository _customerReadRepository;
        private readonly ICustomerSqlRepository _customerSqlReadRepository;

        public GetByEmailAndPassHandler(ICustomerMongoRepository customerReadRepository, ICustomerSqlRepository customerSqlReadRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerSqlReadRepository = customerSqlReadRepository;
        }

        public async Task<Result<CustomerDto>> Handle(GetByEmailAndPassQuery request, CancellationToken cancellationToken)
        {
            var customerMongo = await _customerReadRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (customerMongo == null)
            {
                return Result.Failure<CustomerDto>(CustomerErrors.InvalidCredentials);
            }

            var customerSql = await _customerSqlReadRepository.GetByIdAsync(customerMongo.Id, cancellationToken);

            if (customerSql == null)
            {
                return Result.Failure<CustomerDto>(CustomerErrors.InvalidCredentials);
            }

            var isPasswordValid = customerSql.Password.Verify(request.Password);
            if (!isPasswordValid)
            {
                return Result.Failure<CustomerDto>(CustomerErrors.InvalidCredentials);
            }

            return Result.Success(customerMongo);
        }
    }
}
