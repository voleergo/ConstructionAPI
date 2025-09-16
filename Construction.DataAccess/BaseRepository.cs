using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Construction.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public abstract class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DatabaseConnectionHelper _connectionHelper;
        protected abstract string TableName { get; }
        protected abstract string IdColumn { get; }

        protected BaseRepository(DatabaseConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public abstract Task<IEnumerable<T>> GetAllAsync();
        public abstract Task<T?> GetByIdAsync(long id);
        public abstract Task<long> AddAsync(T entity);
        public abstract Task<bool> UpdateAsync(T entity);
        public abstract Task<bool> DeleteAsync(long id);

        protected async Task<T?> GetSingleStoredProcAsync(string storedProcName, object? parameters = null)
        {
            return await Task.Run(() =>
            {
                Database db = _connectionHelper.GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
                dbCommand.CommandTimeout = 0;
                
                if (parameters != null)
                {
                    AddParameters(db, dbCommand, parameters);
                }

                try
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            return MapFromReader(dataReader);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return null;
            });
        }

        protected async Task<IEnumerable<T>> GetMultipleStoredProcAsync(string storedProcName, object? parameters = null)
        {
            return await Task.Run(() =>
            {
                var results = new List<T>();
                Database db = _connectionHelper.GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
                dbCommand.CommandTimeout = 0;
                
                if (parameters != null)
                {
                    AddParameters(db, dbCommand, parameters);
                }

                try
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            results.Add(MapFromReader(dataReader));
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return (IEnumerable<T>)results;
            });
        }

        protected async Task<long> ExecuteInsertStoredProcAsync(string storedProcName, object parameters)
        {
            return await Task.Run(() =>
            {
                Database db = _connectionHelper.GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
                dbCommand.CommandTimeout = 0;
                
                AddParameters(db, dbCommand, parameters);
                
                // Add output parameter for ID
                db.AddOutParameter(dbCommand, "@ID", DbType.Int64, 8);
                
                try
                {
                    db.ExecuteNonQuery(dbCommand);
                    return Convert.ToInt64(db.GetParameterValue(dbCommand, "@ID"));
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }

        protected async Task<bool> ExecuteNonQueryStoredProcAsync(string storedProcName, object parameters)
        {
            return await Task.Run(() =>
            {
                Database db = _connectionHelper.GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
                dbCommand.CommandTimeout = 0;
                
                AddParameters(db, dbCommand, parameters);
                
                try
                {
                    var rowsAffected = db.ExecuteNonQuery(dbCommand);
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }

        protected abstract T MapFromReader(IDataReader reader);

        // Helper methods removed - now using Convert.To... pattern directly in MapFromReader methods

        private void AddParameters(Database db, DbCommand dbCommand, object parameters)
        {
            var properties = parameters.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(parameters) ?? DBNull.Value;
                var dbType = GetDbType(prop.PropertyType);
                db.AddInParameter(dbCommand, $"@{prop.Name}", dbType, value);
            }
        }

        private DbType GetDbType(Type type)
        {
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int16 => DbType.Int16,
                TypeCode.Int32 => DbType.Int32,
                TypeCode.Int64 => DbType.Int64,
                TypeCode.String => DbType.String,
                TypeCode.DateTime => DbType.DateTime,
                TypeCode.Boolean => DbType.Boolean,
                TypeCode.Decimal => DbType.Decimal,
                TypeCode.Double => DbType.Double,
                _ => DbType.String
            };
        }
    }
}
