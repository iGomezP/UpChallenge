using Microsoft.Data.SqlClient;
using System.Data;
using UpBack.Application.Abstractions.Data;

namespace UpBack.Infrastructure.Data
{
    internal sealed class SqlConnectionFactory(string _connectionString) : ISqlConnectionFactory
    {
        private readonly string _connectionString = _connectionString;
        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }
    }
}
