using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Text;


namespace Construction.DataAccess
{
    public static class EnterpriseExtentions
    {
        public static Database GetDatabase(string connectionString)
        {
            return new SqlDatabase(connectionString);
        }
    }
}
