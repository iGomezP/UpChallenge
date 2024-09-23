using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpBack.Domain.Permissions;

namespace UpBack.Infrastructure.Configurations
{
    internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.CreatedDate)
                .IsRequired();

            builder.Property(p => p.ObjectStatus)
                .HasMaxLength(10)
                .IsRequired();

            // Configuración de Title como valor propio (Value Object)
            builder.OwnsOne(p => p.Title, t =>
            {
                t.Property(tt => tt.Value)
                    .HasColumnName("Title")
                    .IsRequired();
            });

            // Configuración de Scope como valor propio (Value Object)
            builder.OwnsOne(p => p.Scope, s =>
            {
                s.Property(sc => sc.Value)
                    .HasColumnName("Scope")
                    .IsRequired();
            });

            // Optimistic concurrency: manejamos las versiones para evitar conflictos
            builder.Property<byte[]>("Version").IsRowVersion();
        }
    }
}
