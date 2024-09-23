using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpBack.Domain.Customers;

namespace UpBack.Infrastructure.Configurations
{
    internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.BirthDay)
            .IsRequired();

            builder.Property(c => c.CreatedDate)
                .IsRequired();

            builder.Property(c => c.ObjectStatus)
                .HasMaxLength(10)
                .IsRequired();

            builder.OwnsOne(c => c.Name, n =>
            {
                n.Property(n => n.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.LastName, ln =>
            {
                ln.Property(ln => ln.Value)
                    .HasColumnName("LastName")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.Email, e =>
            {
                e.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(100)
                    .IsRequired();

                e.HasIndex(e => e.Value)
                .IsUnique();
            });

            builder.OwnsOne(c => c.PhoneNumber, pn =>
            {
                pn.Property(pn => pn.Value)
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(15)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.Address, a =>
            {
                a.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasMaxLength(100)
                    .IsRequired();

                a.Property(a => a.City)
                    .HasColumnName("City")
                    .HasMaxLength(50)
                    .IsRequired();

                a.Property(a => a.State)
                    .HasColumnName("State")
                    .HasMaxLength(50)
                    .IsRequired();

                a.Property(a => a.ZipCode)
                    .HasColumnName("ZipCode")
                    .HasMaxLength(10)
                    .IsRequired();

                a.Property(a => a.Country)
                    .HasColumnName("Country")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.Property(c => c.RoleId)
                .IsRequired()
                .HasColumnName("RoleId");

            builder.OwnsOne(c => c.Password, p =>
            {
                p.Property(p => p.HashedPassword)
                    .HasColumnName("Password")
                    .HasMaxLength(256)
                    .IsRequired();
            });

            // Optimistic concurrency: si dos clientes envian un request para actualizar lo mismo
            // entonces bloqueamos el record hasta que se complete y luego atender el siguiente
            builder.Property<byte[]>("Version").IsRowVersion();
        }
    }
}
