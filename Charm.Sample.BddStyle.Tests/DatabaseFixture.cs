using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Xunit;

namespace Charm.Sample.BddStyle.Tests
{
    public class DatabaseFixture : IDisposable, IConnectionFactory
    {
        private readonly string _dbname;
        private readonly string _connectionString;

        public DatabaseFixture()
        {
            _dbname = $"auth_service_{Guid.NewGuid().ToString().Replace("-", "")}";
            _connectionString = $"Server=localhost; Port=5432; Database={_dbname}; User ID=integration_test_user; Password=postgres; Pooling=false;";

            // loop through flyway scripts.
            string[] files =
                Directory.GetFiles("../../../../db");

            using (var connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=postgres; User ID=postgres; Password=postgres; Pooling=false;"))
            {
                connection.Execute($"CREATE DATABASE {_dbname};");
                Console.WriteLine($"Database {_dbname} created.");
            }
            using (var connection = GetOpenConnection())
            {
                foreach (var file in files)
                {
                    var sql = File.ReadAllText(file);
                    Console.WriteLine(sql);
                    connection.Execute(sql);
                }
                Console.WriteLine($"Database {_dbname} built.");
            }
        }

        public void Dispose()
        {
            using (var connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=postgres; User ID=postgres; Password=postgres; Pooling=false;"))
            {
                connection.Execute(@$"DROP DATABASE {_dbname};");
                Console.WriteLine($"Database {_dbname} dropped.");
            }
        }

        public DbConnection GetOpenConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
