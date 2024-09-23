namespace UpBack.Api.CommandToRequest
{
    public sealed record PermissionRequest(
        string Title,
        string Scope
        );
}
