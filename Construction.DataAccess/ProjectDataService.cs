using Microsoft.Extensions.Options;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Runtime;
using Construction;
using Construction.DomainModel;
using Construction.DomainModel.User;
using Construction.DomainModel.HttpResponse;
using System.Reflection;
using Construction.DataAccess;
using Newtonsoft.Json;
using Construction.Common;

namespace Construction.DataAccess
{
    public class ProjectDataService 
    {
        private readonly string _connectionString;

        public ProjectDataService(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
