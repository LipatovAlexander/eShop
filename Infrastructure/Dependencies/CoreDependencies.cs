using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Dependencies;
public static class CoreDependencies
{
    public static IServiceCollection AddCoreServices (this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        return services;
    }
}
