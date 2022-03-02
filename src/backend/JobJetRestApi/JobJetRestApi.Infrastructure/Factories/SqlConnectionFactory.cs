using System;
using System.Data;
using JobJetRestApi.Infrastructure.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace JobJetRestApi.Infrastructure.Factories
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly DatabaseOptions _options;
        private readonly string _connectionString;
        private IDbConnection _connection;

        public SqlConnectionFactory(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
            _connectionString = _options.DefaultConnection;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection is null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }

        public void Dispose()
        {
            if (_connection is not null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }
    }
}