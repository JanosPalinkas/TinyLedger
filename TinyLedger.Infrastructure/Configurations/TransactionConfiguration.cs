using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyLedger.Domain;

namespace TinyLedger.Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount)
               .HasPrecision(18, 2);

        builder.Property(t => t.Type)
               .IsRequired();

        builder.Property(t => t.Description)
               .HasMaxLength(500);
    }
}