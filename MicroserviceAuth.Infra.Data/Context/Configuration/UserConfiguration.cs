using MicroserviceAuth.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceAuth.Infra.Data.Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(450).IsRequired();

        builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").IsRequired();

        builder.Property(e => e.EditedBy).HasColumnName("EditedBy").HasMaxLength(450).IsRequired();

        builder.Property(e => e.EditedAt).HasColumnName("EditedAt").IsRequired();
    }
}
