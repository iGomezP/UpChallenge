using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpBack.Domain.Roles;

namespace UpBack.Infrastructure.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.CreatedDate)
                .IsRequired();

            builder.Property(r => r.ObjectStatus)
                .HasMaxLength(10)
                .IsRequired();

            // Configuración de Title como valor propio (Value Object)
            builder.OwnsOne(p => p.Title, t =>
            {
                t.Property(tt => tt.Value)  // Asume que Title tiene una propiedad Value
                    .HasColumnName("Title")
                    .IsRequired();
            });

            // Configuración de Permissions como una colección
            builder
                .HasMany(r => r.Permissions)
                .WithOne() // Asumiendo que no hay relación bidireccional
                .OnDelete(DeleteBehavior.Cascade); // En cascada si se elimina el rol

            // Optimistic concurrency: manejamos las versiones para evitar conflictos
            builder.Property<byte[]>("Version").IsRowVersion();
        }
    }
}
