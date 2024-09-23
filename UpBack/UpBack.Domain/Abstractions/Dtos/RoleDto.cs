namespace UpBack.Domain.Abstractions.Dtos
{
    public sealed class RoleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<PermissionsDto> Permissions { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ObjectStatus { get; set; } = "active";
    }

    public sealed class PermissionsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Scope { get; set; }
    }
}
