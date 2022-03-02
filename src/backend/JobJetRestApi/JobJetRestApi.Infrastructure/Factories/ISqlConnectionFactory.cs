using System.Data;

namespace JobJetRestApi.Infrastructure.Factories
{
    public interface ISqlConnectionFactory // @TODO - where to store interfaces like this one?
    {
        IDbConnection GetOpenConnection();
    }
}