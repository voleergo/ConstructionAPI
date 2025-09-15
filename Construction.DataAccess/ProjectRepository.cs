using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        protected override string TableName => "Projects";
        protected override string IdColumn => "ID_Project";

        public ProjectRepository(DatabaseConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public override async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await GetMultipleStoredProcAsync("usp_Project_GetAll");
        }

        public override async Task<Project> GetByIdAsync(long id)
        {
            return await GetSingleStoredProcAsync("usp_Project_GetById", new { ID_Project = id });
        }

        public override async Task<long> AddAsync(Project project)
        {
            return await ExecuteInsertStoredProcAsync("usp_Project_Insert", new
            {
                project.ProjectCode,
                project.ProjectName,
                project.FK_Customer,
                project.EstimateAmt,
                project.StartDate,
                project.EndDate,
                project.ProjectType,
                project.ProjectStatus,
                project.CreatedBy,
                project.CreatedOn,
                project.UpdatedBy,
                project.UpdatedOn
            });
        }

        public override async Task<bool> UpdateAsync(Project project)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Project_Update", new
            {
                project.ID_Project,
                project.ProjectCode,
                project.ProjectName,
                project.FK_Customer,
                project.EstimateAmt,
                project.StartDate,
                project.EndDate,
                project.ProjectType,
                project.ProjectStatus,
                project.UpdatedBy,
                project.UpdatedOn
            });
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_Project_Delete", new { ID_Project = id });
        }

        public async Task<Project> GetByProjectCodeAsync(string projectCode)
        {
            return await GetSingleStoredProcAsync("usp_Project_GetByCode", new { ProjectCode = projectCode });
        }

        public async Task<IEnumerable<Project>> GetProjectsByCustomerAsync(long customerId)
        {
            return await GetMultipleStoredProcAsync("usp_Project_GetByCustomer", new { FK_Customer = customerId });
        }

        public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(string status)
        {
            return await GetMultipleStoredProcAsync("usp_Project_GetByStatus", new { ProjectStatus = status });
        }

        protected override Project MapFromReader(IDataReader reader)
        {
            return new Project
            {
                ID_Project = reader.GetInt64("ID_Project"),
                ProjectCode = reader.GetString("ProjectCode"),
                ProjectName = reader.GetString("ProjectName"),
                FK_Customer = GetNullableLong(reader, "FK_Customer"),
                EstimateAmt = GetNullableDecimal(reader, "EstimateAmt"),
                StartDate = GetNullableDateTime(reader, "StartDate"),
                EndDate = GetNullableDateTime(reader, "EndDate"),
                ProjectType = GetNullableString(reader, "ProjectType"),
                ProjectStatus = GetNullableString(reader, "ProjectStatus"),
                CreatedBy = GetNullableLong(reader, "CreatedBy"),
                CreatedOn = reader.GetDateTime("CreatedOn"),
                UpdatedBy = GetNullableLong(reader, "UpdatedBy"),
                UpdatedOn = GetNullableDateTime(reader, "UpdatedOn")
            };
        }
    }
}
