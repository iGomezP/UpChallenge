using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpBack.Domain.Accounts;

namespace UpBack.Infrastructure.Configurations
{
    namespace UpBack.Infrastructure.Configurations
    {
        internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
        {
            public void Configure(EntityTypeBuilder<Account> builder)
            {
                builder.ToTable("Accounts");
                builder.HasKey(a => a.Id);

                builder.Property(a => a.CreatedDate)
                    .IsRequired();

                builder.Property(a => a.ObjectStatus)
                    .HasMaxLength(10)
                    .IsRequired();

                builder.Property(a => a.MovReference)
                    .HasMaxLength(30)
                    .IsRequired();

                builder.OwnsOne(a => a.Number, n =>
                {
                    n.Property(n => n.Value)
                        .HasColumnName("AccountNumber")
                        .HasMaxLength(20)
                        .IsRequired();

                    n.HasIndex(n => n.Value)
                        .IsUnique();
                });

                builder.OwnsOne(a => a.Balance, b =>
                {
                    b.Property(b => b.Value)
                        .HasColumnName("Balance")
                        .IsRequired()
                        .HasColumnType("decimal(18, 2)");
                });

                // Relación con el Customer
                builder.HasOne(a => a.Customer)
                    .WithMany()
                    .HasForeignKey(a => a.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict); // Opción para manejo de eliminación

                // Optimistic concurrency control
                builder.Property<byte[]>("Version").IsRowVersion();
            }
        }
    }

}
