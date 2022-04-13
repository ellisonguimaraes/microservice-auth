using MicroserviceAuth.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceAuth.Infra.Data.Context.Configuration;

public class ApplicationUserConfiguration : BaseEntityConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUser");

        builder.HasKey(au => new { au.UserId, au.ApplicationId });

        builder.Property(au => au.UserId).HasColumnName("UserId").HasMaxLength(450).IsRequired();
        builder.HasOne(au => au.User)
            .WithMany(u => u.ApplicationUsers)
            .HasForeignKey(au => au.UserId);

        builder.Property(au => au.ApplicationId).HasColumnName("ApplicationId").HasMaxLength(450).IsRequired();
        builder.HasOne(au => au.Application)
            .WithMany(a => a.ApplicationUsers)
            .HasForeignKey(au => au.ApplicationId);

        base.Configure(builder);
    }
}
