using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Core.Interfaces;
using TaskManagement.Data.DbContexts;

namespace TaskManagement.Data;

public static class DependencyInjections
{
    public static IServiceCollection AddRepositories(
    this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        foreach (var x in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
            .Where(t => !t.IsInterface && !t.IsGenericType && !t.IsAbstract)
            .Select(t => new { Type = t, Interfaces = t.GetInterfaces() })
            .Select(x => new
            {
                Type = x.Type,
                EntityType = x.Interfaces.FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepositoryBase<>))
                    ?.GetGenericArguments().FirstOrDefault(),
                Interfaces = x.Interfaces.Where(i => !i.IsGenericType),
            })
            .Where(x => x.EntityType != null))
        {
            foreach (Type i in x.Interfaces)
            {
                services.AddScoped(i, x.Type);
            }
        }

        return services;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
        .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}