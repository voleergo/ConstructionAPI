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
        protected readonly Database _database;
        protected abstract string TableName { get; }
        protected abstract string IdColumn { get; }

        protected BaseRepository(DatabaseConnectionHelper connectionHelper)
        {
            _database = connectionHelper.GetDatabase();
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
                using var command = _database.GetStoredProcCommand(storedProcName);
                
                if (parameters != null)
                {
                    AddParameters(command, parameters);
                }

                using var reader = _database.ExecuteReader(command);
                if (reader.Read())
                {
                    return MapFromReader(reader);
                }
                return null;
            });
        }

        protected async Task<IEnumerable<T>> GetMultipleStoredProcAsync(string storedProcName, object? parameters = null)
        {
            return await Task.Run(() =>
            {
                var results = new List<T>();
                using var command = _database.GetStoredProcCommand(storedProcName);
                
                if (parameters != null)
                {
                    AddParameters(command, parameters);
                }

                using var reader = _database.ExecuteReader(command);
                while (reader.Read())
                {
                    results.Add(MapFromReader(reader));
                }
                return (IEnumerable<T>)results;
            });
        }

        protected async Task<long> ExecuteInsertStoredProcAsync(string storedProcName, object parameters)
        {
            return await Task.Run(() =>
            {
                using var command = _database.GetStoredProcCommand(storedProcName);
                
                AddParameters(command, parameters);
                
                // Add output parameter for ID
                _database.AddOutParameter(command, "@ID", DbType.Int64, 8);
                
                _database.ExecuteNonQuery(command);
                return Convert.ToInt64(_database.GetParameterValue(command, "@ID"));
            });
        }

        protected async Task<bool> ExecuteNonQueryStoredProcAsync(string storedProcName, object parameters)
        {
            return await Task.Run(() =>
            {
                using var command = _database.GetStoredProcCommand(storedProcName);
                
                AddParameters(command, parameters);
                
                var rowsAffected = _database.ExecuteNonQuery(command);
                return rowsAffected > 0;
            });
        }

        protected abstract T MapFromReader(IDataReader reader);

        protected static long? GetNullableLong(IDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetInt64(ordinal);
        }

        protected static DateTime? GetNullableDateTime(IDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetDateTime(ordinal);
        }

        protected static string? GetNullableString(IDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        protected static decimal? GetNullableDecimal(IDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetDecimal(ordinal);
        }

        private void AddParameters(DbCommand command, object parameters)
        {
            var properties = parameters.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(parameters) ?? DBNull.Value;
                _database.AddInParameter(command, $"@{prop.Name}", DbType.Object, value);
            }
        }
    }
}
