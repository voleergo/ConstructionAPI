using Construction.Common;
using Construction.DomainModel;
using Construction.DomainModel.Item; // Use the actual namespace
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Construction.DataAccess
{
    public class ItemDataService
    {
        private readonly string _connectionString;
        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        public ItemDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

       
    }
}
