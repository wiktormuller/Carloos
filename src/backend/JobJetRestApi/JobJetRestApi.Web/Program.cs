using System;
using System.Linq;
using System.Threading.Tasks;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using JobJetRestApi.Web.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace JobJetRestApi.Web
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            // var seed = args.Contains("/seed");
            // if (seed)
            // {
            //     args = args.Except(new[] {"/seed"}).ToArray();
            // }

            var host = CreateHostBuilder(args)
                .ConfigureAppConfiguration(x => x.AddEnvironmentVariables(prefix: "JobJetVariables_"))
                .Build();
            
            if (true)
            {
                try
                {
                    Log.Information("Applying migrations...");
                    await host.ApplyMigrations<JobJetDbContext>();
                    Log.Information("Done applying migrations.");
                    
                    Log.Information("Seeding database...");
                    await host.SeedJobJetContext<JobJetDbContext>();
                    Log.Information("Done seeding database.");
                }
                catch (Exception e)
                {
                    Log.Error(e, "An error occurred while applying-migrations/seeding the database.");
                }
            }

            Log.Information("Starting host...");
            await host.RunAsync();

            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}