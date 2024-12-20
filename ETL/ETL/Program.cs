using ETL;
using ETL.Entities;
using ETL.Interfaces;
using ETL.Mappers;
using ETL.Repositories;
using ETL.Response;
using ETL.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        var taxiTripService = host.Services.GetRequiredService<ITaxiTripService>();

        var fileArg = args.FirstOrDefault(arg => arg.StartsWith("--file="));
        string fileName;

        if (fileArg != null)
        {
            fileName = fileArg.Split("=")[1];
        }
        else
        {
            logger.LogError("Error: The '--file=' argument is required. Please specify the file path.");
            return;
        }

        var result = await taxiTripService.ProcessTripsAsync(fileName);

        if (result.IsSuccess)
        {
            logger.LogInformation("Data migration completed successfully.");
        }
        else
        {
            logger.LogError($"Data migration failed: {result.ErrorMessage}");
        }

    }
    

    static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((context, services) =>
        {
            services.AddScoped<ITaxiTripRepository>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new TaxiTripRepository(connectionString);
            });
            services.AddValidatorsFromAssemblyContaining<TaxiTripValidator>();
            services.AddScoped<ITaxiTripService, TaxiTripService>();

            services.AddLogging();
        });
}
