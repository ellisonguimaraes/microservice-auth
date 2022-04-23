using Serilog;

namespace MicroserviceAuth.API.Configurations;

public static class SerilogConfiguration
{
    public static void AddSerilogConfiguration(this WebApplicationBuilder builder)
    {
        // Remove default logging providers
        builder.Logging.ClearProviders();

        // Serilog configuration		
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        // Register Serilog
        builder.Logging.AddSerilog(logger);
    }
}
