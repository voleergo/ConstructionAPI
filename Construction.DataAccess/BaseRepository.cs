using Microsoft.Data.SqlClient;
using System.Data;
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
            using var connection = (SqlConnection)_connectionHelper.CreateConnection();
            await connection.OpenAsync();
            
            using var command = new SqlCommand(storedProcName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            if (parameters != null)
            {
                AddParameters(command, parameters);
            }

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapFromReader(reader);
            }
            return null;
        }

        protected async Task<IEnumerable<T>> GetMultipleStoredProcAsync(string storedProcName, object? parameters = null)
        {
            var results = new List<T>();
            using var connection = (SqlConnection)_connectionHelper.CreateConnection();
            await connection.OpenAsync();
            
            using var command = new SqlCommand(storedProcName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            if (parameters != null)
            {
                AddParameters(command, parameters);
            }

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                results.Add(MapFromReader(reader));
            }
            return results;
        }

        protected async Task<long> ExecuteInsertStoredProcAsync(string storedProcName, object parameters)
        {
            using var connection = (SqlConnection)_connectionHelper.CreateConnection();
            await connection.OpenAsync();
            
            using var command = new SqlCommand(storedProcName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            AddParameters(command, parameters);
            
            // Add output parameter for ID
            var outputParam = new SqlParameter("@ID", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputParam);
            
            await command.ExecuteNonQueryAsync();
            return Convert.ToInt64(outputParam.Value);
        }

        protected async Task<bool> ExecuteNonQueryStoredProcAsync(string storedProcName, object parameters)
        {
            using var connection = (SqlConnection)_connectionHelper.CreateConnection();
            await connection.OpenAsync();
            
            using var command = new SqlCommand(storedProcName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            AddParameters(command, parameters);
            
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
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

        private void AddParameters(SqlCommand command, object parameters)
        {
            var properties = parameters.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(parameters) ?? DBNull.Value;
                command.Parameters.AddWithValue($"@{prop.Name}", value);
            }
        }
    }
}
