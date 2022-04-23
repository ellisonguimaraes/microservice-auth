using MicroserviceAuth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceAuth.Infra.Data.Context.Configuration;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(450).IsRequired();

        builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").IsRequired();

        builder.Property(e => e.EditedBy).HasColumnName("EditedBy").HasMaxLength(450).IsRequired();

        builder.Property(e => e.EditedAt).HasColumnName("EditedAt").IsRequired();
    }
}
