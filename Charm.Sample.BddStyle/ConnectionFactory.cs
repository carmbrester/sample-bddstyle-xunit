using System.Data.Common;
using Npgsql;

namespace Charm.Sample.BddStyle
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string dbServer,
            string dbPort,
            string dbName,
            string dbUser,
            string dbPassword)
        {
            _connectionString =
                $"Server={dbServer}; "
                + $"Port={dbPort ?? "5432"}; "
                + $"Database={dbName}; "
                + $"User ID={dbUser}; "
                + $"Password={dbPassword};enlist=true";

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public DbConnection GetOpenConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
