namespace UpBack.Api.CommandToRequest
{
    public sealed record AccountRequest(
        Guid CustomerId,
        string AccountNumber,
        decimal InitialBalance,
        string Reference
        );
}
