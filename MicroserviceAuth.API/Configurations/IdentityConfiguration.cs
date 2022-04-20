using MicroserviceAuth.Domain.Identity;
using MicroserviceAuth.Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MicroserviceAuth.API.Configurations;

public static class IdentityConfiguration
{
    public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Identity settings
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Lockout = new LockoutOptions
            {
                AllowedForNewUsers = true,
                DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10),
                MaxFailedAccessAttempts = 3
            };
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        // Authentication and Authorization settings
        services.AddAuthorization(options =>
        {
            options.AddPolicy(RoleSettings.EGRESS, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.EGRESS).RequireClaim("ACTIVATED", "true"));
            options.AddPolicy(RoleSettings.TEACHER, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.TEACHER).RequireClaim("ACTIVATED", "true"));
            options.AddPolicy(RoleSettings.STUDENT, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.STUDENT).RequireClaim("ACTIVATED", "true"));
            options.AddPolicy(RoleSettings.COLLEGIATE, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.COLLEGIATE).RequireClaim("ACTIVATED", "true").RequireClaim(RoleSettings.SECTION_CLAIM_NAME));
            options.AddPolicy(RoleSettings.DEPARTMENT, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.DEPARTMENT).RequireClaim("ACTIVATED", "true").RequireClaim(RoleSettings.SECTION_CLAIM_NAME));
            options.AddPolicy(RoleSettings.ADMINISTRATOR, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.ADMINISTRATOR).RequireClaim("ACTIVATED", "true"));
        });

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateActor = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtBearerTokenSettings:Issuer"],
                ValidAudience = configuration["JwtBearerTokenSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]))
            };
        });
    }
}
