using MicroserviceAuth.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceAuth.Infra.Data.Context.Configuration;

public class ApplicationConfiguration : BaseEntityConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Application");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasColumnName("Id")
            .HasMaxLength(450)
            .IsRequired();

        builder.HasIndex(a => a.ApiKey).IsUnique();
        builder.Property(a => a.ApiKey).HasColumnName("ApiKey").HasMaxLength(100).IsRequired();

        builder.HasIndex(a => a.ApplicationName).IsUnique();
        builder.Property(a => a.ApplicationName).HasColumnName("ApplicationName").HasMaxLength(100).IsRequired();

        base.Configure(builder);
    }
}
