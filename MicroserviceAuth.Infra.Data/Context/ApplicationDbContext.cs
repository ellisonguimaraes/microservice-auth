using MicroserviceAuth.Domain.Application;
using MicroserviceAuth.Domain.Identity;
using MicroserviceAuth.Infra.Data.Context.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceAuth.Infra.Data.Context;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Application> Applications { get; set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    { 
        new UserConfiguration().Configure(builder.Entity<User>());
        new ApplicationConfiguration().Configure(builder.Entity<Application>());
        new ApplicationUserConfiguration().Configure(builder.Entity<ApplicationUser>());

        base.OnModelCreating(builder);
    }
}