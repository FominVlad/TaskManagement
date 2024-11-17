using Microsoft.Extensions.DependencyInjection;
using TaskManagement.BL.Helpers;
using TaskManagement.BL.Services;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Interfaces.Services;
using TaskManagement.Data;
using TaskManagement.ServiceBus.Services;

namespace TaskManagement.AzureFunctions.TaskProcessor;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IValidator, Validator>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IServiceBusService, ServiceBusService>()
            .AddScoped<ITaskService, TaskService>();
    }
}