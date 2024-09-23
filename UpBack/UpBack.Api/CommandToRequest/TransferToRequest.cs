namespace UpBack.Api.CommandToRequest
{
    public sealed record TransferToRequest(
        Guid TargetAccountId,
        decimal Amount,
        string Reference
        );
}
