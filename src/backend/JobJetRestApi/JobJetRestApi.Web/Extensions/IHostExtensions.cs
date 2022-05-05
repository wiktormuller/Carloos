using System.Threading.Tasks;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using JobJetRestApi.Infrastructure.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JobJetRestApi.Web.Extensions
{
    public static class IHostExtensions
    {
        public static async Task<IHost> SeedJobJetContext<TContext>(this IHost host) where TContext : JobJetDbContext
        {
            // Create a scope to get scoped services.
            using var scope = host.Services.CreateScope();
            
            var services = scope.ServiceProvider;
            var context = services.GetService<JobJetDbContext>();
            var userRepository = services.GetService<IUserRepository>();
            var roleRepository = services.GetService<IRoleRepository>();
            
            await JobJetContextSeeder.SeedAsync(context, userRepository, roleRepository);

            return host;
        }

        public static async Task<IHost> ApplyMigrations<TContext>(this IHost host) where TContext : JobJetDbContext
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var context = services.GetService<JobJetDbContext>();

            await context.Database.MigrateAsync();

            return host;
        }
    }
}