using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Dependencies;
public static class DbContextDependencies
{
    public static IServiceCollection AddApplicationDbContext (this IServiceCollection services, string connectionString, Action<DbContextOptionsBuilder> builder = null)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            builder?.Invoke(options);
        });

        return services;
    }
}
