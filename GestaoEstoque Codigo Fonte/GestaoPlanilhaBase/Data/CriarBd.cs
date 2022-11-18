using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoPlanilhaBase.Data
{
    public static class CriarBd
    {
        public static void CriarDatabase<TContext>(this IServiceCollection services, IConfiguration configuration, string connectionString) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);

            using (IServiceScope serviceScope = services.BuildServiceProvider().CreateScope())
            {
                using (DbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<TContext>())
                {
                    dbContext.Database.EnsureCreated();
                }
            }
        }
    }
}
