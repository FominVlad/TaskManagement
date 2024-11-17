using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Core.Interfaces.Services;
using TaskManagement.ServiceBus.Services;

namespace TaskManagement.ServiceBus;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceBus(this IServiceCollection services)
    {
        return services
            .AddScoped<IServiceBusService, ServiceBusService>();
    }
}