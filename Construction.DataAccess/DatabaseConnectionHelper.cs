using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;

namespace Construction.DataAccess
{
    public class DatabaseConnectionHelper
    {
        private readonly Database _database;

        public DatabaseConnectionHelper(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' not found.");
            _database = new SqlDatabase(connectionString);
        }

        public Database GetDatabase()
        {
            return _database;
        }
    }
}
