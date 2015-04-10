using System;

using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

using Npgsql;

namespace Npgsql
{
    public class NpgsqlEFConfiguration : DbConfiguration
    {
        public NpgsqlEFConfiguration()
        {
            SetProviderServices("Npgsql", NpgsqlServices.Instance);
            SetProviderFactory("Npgsql", NpgsqlFactory.Instance);
            SetDefaultConnectionFactory(new NpgsqlConnectionFactory());
            SetMigrationSqlGenerator("Npgsql", () => new NpgsqlMigrationSqlGenerator());
            SetHistoryContext("Npgsql", (conn, schema) => CreateHistoryContext(conn, schema));
        }
        
        private HistoryContext CreateHistoryContext(DbConnection connection, String defaultSchema)
        {
            return new HistoryContext(connection, "public");
        }
    }
}