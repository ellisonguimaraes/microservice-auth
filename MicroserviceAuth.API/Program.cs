using MicroserviceAuth.API.Configurations;
using MicroserviceAuth.Infra.CrossCutting.IoC;
using MicroserviceAuth.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger Configuration.
builder.Services.AddSwaggerConfiguration();

// Database Configuration.
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("AuthDbConnectionString"));

// Identity Configuration.
builder.Services.AddIdentityConfiguration(builder.Configuration);

// Dependency Injector.
builder.Services.RegisterServices();

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

app.MapControllers();

app.Run();