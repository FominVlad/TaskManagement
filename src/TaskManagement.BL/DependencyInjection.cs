using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.BL.Helpers;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.BL;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        foreach (var x in Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => !t.IsInterface && !t.IsGenericType && !t.IsAbstract)
            .Select(t => new { Type = t, Interfaces = t.GetInterfaces().Where(i => !i.IsGenericType) })
            .Where(x => x.Type.Namespace != null &&
                        x.Type.Namespace.Equals("TaskManagement.BL.Services") &&
                        x.Type.Name.EndsWith("Service")))
        {
            foreach (Type i in x.Interfaces)
            {
                services.AddScoped(i, x.Type);
            }
        }

        services.AddSingleton<IValidator, Validator>();

        return services;
    }
}