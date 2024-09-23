using System.Text.Json.Serialization;

namespace UpBack.Domain.Transactions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionStatusEnum
    {
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Cancelled = 3,
        Rejected = 4,
        Refunded = 5,
    }
}
