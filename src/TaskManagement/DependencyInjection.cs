using TaskManagement.Filters;

namespace TaskManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        return services
            .AddTransient<ExceptionFilter>();
    }
}