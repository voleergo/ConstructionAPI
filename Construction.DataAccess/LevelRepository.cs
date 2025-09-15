using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class LevelRepository : BaseRepository<Level>, ILevelRepository
    {
        protected override string TableName => "Levels";
        protected override string IdColumn => "ID_Level";

        public LevelRepository(DatabaseConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public override async Task<IEnumerable<Level>> GetAllAsync()
        {
            return await GetMultipleStoredProcAsync("usp_Level_GetAll");
        }

        public override async Task<Level?> GetByIdAsync(long id)
        {
            return await GetSingleStoredProcAsync("usp_Level_GetById", new { ID_Level = id });
        }

        public override async Task<long> AddAsync(Level level)
        {
            return await ExecuteInsertStoredProcAsync("usp_Level_Insert", new
            {
                level.LevelCode,
                level.LevelName,
                level.LevelStatus,
                level.CreatedBy,
                level.CreatedOn,
                level.UpdatedBy,
                level.UpdatedOn
            });
        }

        public override async Task<bool> UpdateAsync(Level level)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Level_Update", new
            {
                level.ID_Level,
                level.LevelCode,
                level.LevelName,
                level.LevelStatus,
                level.UpdatedBy,
                level.UpdatedOn
            });
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Level_Delete", new { ID_Level = id });
        }

        public async Task<Level?> GetByLevelCodeAsync(string levelCode)
        {
            return await GetSingleStoredProcAsync("usp_Level_GetByCode", new { LevelCode = levelCode });
        }

        public async Task<IEnumerable<Level>> GetLevelsByStatusAsync(string status)
        {
            return await GetMultipleStoredProcAsync("usp_Level_GetByStatus", new { LevelStatus = status });
        }

        protected override Level MapFromReader(IDataReader reader)
        {
            return new Level
            {
                ID_Level = reader.GetInt64(reader.GetOrdinal("ID_Level")),
                LevelCode = reader.GetString(reader.GetOrdinal("LevelCode")),
                LevelName = reader.GetString(reader.GetOrdinal("LevelName")),
                LevelStatus = GetNullableString(reader, "LevelStatus"),
                CreatedBy = GetNullableLong(reader, "CreatedBy"),
                CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                UpdatedBy = GetNullableLong(reader, "UpdatedBy"),
                UpdatedOn = GetNullableDateTime(reader, "UpdatedOn")
            };
        }
    }
}
