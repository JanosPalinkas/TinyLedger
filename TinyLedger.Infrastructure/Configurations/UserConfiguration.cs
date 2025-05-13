using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyLedger.Domain;

namespace TinyLedger.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.ToTable("Users");

       builder.HasKey(u => u.Id);

       builder.Property(u => u.Id)
              .IsRequired();

       builder.Property(u => u.Email)
              .IsRequired()
              .HasMaxLength(256);

       builder.Property(u => u.Name)
              .IsRequired()
              .HasMaxLength(256);

       builder.Property(u => u.AccountId)
              .IsRequired();

       builder.Property(u => u.PasswordHash)
              .IsRequired()
              .HasMaxLength(200);

       builder.Property(u => u.Role)
              .IsRequired()
              .HasMaxLength(50);
    }
}