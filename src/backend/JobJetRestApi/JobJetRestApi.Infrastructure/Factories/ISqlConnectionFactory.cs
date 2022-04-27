using System.Data;

namespace JobJetRestApi.Infrastructure.Factories
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}