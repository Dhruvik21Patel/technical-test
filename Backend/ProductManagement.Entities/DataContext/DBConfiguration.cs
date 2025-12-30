using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductManagement.Entities.DataContext
{
    public static class DBConfiguration
    {
        public static void RegisterDatabaseConnection(
   this IServiceCollection services,
   IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    config.GetConnectionString("DefaultConnection")
                ));
        }
    }
}