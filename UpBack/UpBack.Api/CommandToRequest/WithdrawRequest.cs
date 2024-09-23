namespace UpBack.Api.CommandToRequest
{
    public sealed record WithdrawRequest(
        decimal Amount,
        string Reference
        );
}
