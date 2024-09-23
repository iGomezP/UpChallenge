namespace UpBack.Api.CommandToRequest
{
    public sealed record RoleRequest(
        string Title,
        IEnumerable<PermissionRequest> Permissions,
        string ObjectStatus
    );


}
