using MicroserviceAuth.API.Configurations;
using MicroserviceAuth.API.Middlewares;
using MicroserviceAuth.Infra.CrossCutting.IoC;
using MicroserviceAuth.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger Configuration.
builder.Services.AddSwaggerConfiguration();

// Database Configuration.
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("AuthDbConnectionString"));

// Identity Configuration.
builder.Services.AddIdentityConfiguration(builder.Configuration);

// AutoMapper Configuration
builder.Services.AddAutoMapperConfiguration();

// Serilog Configuration.
builder.AddSerilogConfiguration();

// Dependency Injector.
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = String.Empty;
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();