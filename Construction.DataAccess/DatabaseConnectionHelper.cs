using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;

namespace Construction.DataAccess
{
    public class DatabaseConnectionHelper
    {
        private readonly string _connectionString;

        public DatabaseConnectionHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' not found.");
            
            // Set up the database provider factory
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
        }

        public Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase(_connectionString);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
