using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Charm.Sample.BddStyle.Tests
{
    public static class RepositoryTestsHelper
    {
        public static int GetTableRowCount(IConnectionFactory connectionFactory, string tableName)
        {
            using (var connection = connectionFactory.GetOpenConnection())
            {
                return connection.ExecuteScalar<int>($"SELECT COUNT(*) FROM {tableName};");
            }
        }
    }
}
