using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class ProjectLevelRepository : BaseRepository<ProjectLevel>, IProjectLevelRepository
    {
        protected override string TableName => "ProjectLevels";
        protected override string IdColumn => "ID_ProjectLevel";

        public ProjectLevelRepository(DatabaseConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public override async Task<IEnumerable<ProjectLevel>> GetAllAsync()
        {
            return await GetMultipleStoredProcAsync("usp_ProjectLevel_GetAll");
        }

        public override async Task<ProjectLevel> GetByIdAsync(long id)
        {
            return await GetSingleStoredProcAsync("usp_ProjectLevel_GetById", new { ID_ProjectLevel = id });
        }

        public override async Task<long> AddAsync(ProjectLevel projectLevel)
        {
            return await ExecuteInsertStoredProcAsync("usp_ProjectLevel_Insert", new
            {
                projectLevel.FK_Project,
                projectLevel.FK_Level
            });
        }

        public override async Task<bool> UpdateAsync(ProjectLevel projectLevel)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_ProjectLevel_Update", new
            {
                projectLevel.ID_ProjectLevel,
                projectLevel.FK_Project,
                projectLevel.FK_Level
            });
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_ProjectLevel_Delete", new { ID_ProjectLevel = id });
        }

        public async Task<IEnumerable<ProjectLevel>> GetByProjectIdAsync(long projectId)
        {
            return await GetMultipleStoredProcAsync("usp_ProjectLevel_GetByProject", new { FK_Project = projectId });
        }

        public async Task<IEnumerable<ProjectLevel>> GetByLevelIdAsync(long levelId)
        {
            return await GetMultipleStoredProcAsync("usp_ProjectLevel_GetByLevel", new { FK_Level = levelId });
        }

        protected override ProjectLevel MapFromReader(IDataReader reader)
        {
            return new ProjectLevel
            {
                ID_ProjectLevel = reader.GetInt64("ID_ProjectLevel"),
                FK_Project = reader.GetInt64("FK_Project"),
                FK_Level = reader.GetInt64("FK_Level")
            };
        }
    }
}
