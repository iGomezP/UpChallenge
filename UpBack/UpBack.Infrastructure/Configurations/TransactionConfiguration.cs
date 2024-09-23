using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpBack.Domain.Transactions;

namespace UpBack.Infrastructure.Configurations
{
    internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TransactionDate)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(a => a.MovReference)
                    .HasMaxLength(30)
                    .IsRequired();

            builder.OwnsOne(t => t.Type, tt =>
            {
                tt.Property(tt => tt.Value)
                    .HasColumnName("TransactionType")
                    .HasMaxLength(10)
                    .IsRequired();
            });

            builder.OwnsOne(t => t.Quantity, q =>
            {
                q.Property(q => q.Value)
                    .HasColumnName("Quantity")
                    .IsRequired()
                    .HasColumnType("decimal(18, 2)");
            });

            // Relación con Account
            builder.HasOne(t => t.Account)
                .WithMany() // Si Account puede tener varias transacciones
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict); // Restricción para evitar eliminar cuentas si tienen transacciones

            // Optimistic concurrency control
            builder.Property<byte[]>("Version").IsRowVersion();
        }
    }
}
