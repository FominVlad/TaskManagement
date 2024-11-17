using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagement.AzureFunctions.TaskProcessor;
using TaskManagement.Data.DbContexts;

string dbConnectionString = Environment.GetEnvironmentVariable("DbConnectionString");

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(dbConnectionString);
        });
        services.AddServices();
    })
    .Build();

host.Run();