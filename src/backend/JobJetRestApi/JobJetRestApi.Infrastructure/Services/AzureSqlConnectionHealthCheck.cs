using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace JobJetRestApi.Infrastructure.Services
{
    public class AzureSqlConnectionHealthCheck : IHealthCheck
    {
        private const string DefaultTestQuery = "SELECT 1";
        
        public string ConnectionString { get; }
        public string TestQuery { get; }

        public AzureSqlConnectionHealthCheck(string connectionString) : this(connectionString, DefaultTestQuery)
        {
        }

        public AzureSqlConnectionHealthCheck(string connectionString, string testQuery)
        {
            ConnectionString = Guard.Against.Null(connectionString, nameof(connectionString));
            TestQuery = testQuery;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    if (TestQuery is not null)
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = TestQuery;

                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
                catch (DbException e)
                {
                    return new HealthCheckResult(status: context.Registration.FailureStatus, exception: e);
                }
            }

            return HealthCheckResult.Healthy();
        }
    }
}