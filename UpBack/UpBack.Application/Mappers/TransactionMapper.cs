using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Transactions;

namespace UpBack.Application.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDto MapToMongoDto(this Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                TransactionType = transaction.Type.Value,
                Quantity = transaction.Quantity.Value,
                Reference = transaction.MovReference,
                Status = transaction.Status,
                TransactionDate = transaction.TransactionDate,
                AccountId = transaction.AccountId
            };
        }
    }
}
