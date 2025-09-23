using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class ProjectRepository
    {
        //protected override string TableName => "Projects";
        //protected override string IdColumn => "ID_Project";

        //public ProjectRepository(DatabaseConnectionHelper connectionHelper) 
        //    : base(connectionHelper)
        //{
        //}

        //public override async Task<IEnumerable<ProjectModel>> GetAllAsync()
        //{
        //    return await GetMultipleStoredProcAsync("usp_Project_GetAll");
        //}

        //public override async Task<ProjectModel?> GetByIdAsync(long id)
        //{
        //    return await GetSingleStoredProcAsync("usp_Project_GetById", new { ID_Project = id });
        //}

        //public override async Task<long> AddAsync(ProjectModel project)
        //{
        //    return await ExecuteInsertStoredProcAsync("usp_Project_Insert", new
        //    {
        //        project.ProjectCode,
        //        project.ProjectName,
        //        project.FK_Customer,
        //        project.EstimateAmt,
        //        project.StartDate,
        //        project.EndDate,
        //        project.ProjectType,
        //        project.ProjectStatus,
        //        project.CreatedBy,
        //        project.CreatedOn,
        //        project.UpdatedBy,
        //        project.UpdatedOn
        //    });
        //}

        //public override async Task<bool> UpdateAsync(ProjectModel project)
        //{
        //    return await ExecuteNonQueryStoredProcAsync("usp_Project_Update", new
        //    {
        //        project.ID_Project,
        //        project.ProjectCode,
        //        project.ProjectName,
        //        project.FK_Customer,
        //        project.EstimateAmt,
        //        project.StartDate,
        //        project.EndDate,
        //        project.ProjectType,
        //        project.ProjectStatus,
        //        project.UpdatedBy,
        //        project.UpdatedOn
        //    });
        //}

        //public override async Task<bool> DeleteAsync(long id)
        //{
        //    return await ExecuteNonQueryStoredProcAsync("usp_Project_Delete", new { ID_Project = id });
        //}

        //public async Task<ProjectModel?> GetByProjectCodeAsync(string projectCode)
        //{
        //    return await GetSingleStoredProcAsync("usp_Project_GetByCode", new { ProjectCode = projectCode });
        //}

        //public async Task<IEnumerable<ProjectModel>> GetProjectsByCustomerAsync(long customerId)
        //{
        //    return await GetMultipleStoredProcAsync("usp_Project_GetByCustomer", new { FK_Customer = customerId });
        //}

        //public async Task<IEnumerable<ProjectModel>> GetProjectsByStatusAsync(string status)
        //{
        //    return await GetMultipleStoredProcAsync("usp_Project_GetByStatus", new { ProjectStatus = status });
        //}

        //protected override ProjectModel MapFromReader(IDataReader dataReader)
        //{
        //    return new ProjectModel
        //    {
        //        ID_Project = Convert.ToInt64(dataReader["ID_Project"]),
        //        ProjectCode = Convert.ToString(dataReader["ProjectCode"]) ?? string.Empty,
        //        ProjectName = Convert.ToString(dataReader["ProjectName"]) ?? string.Empty,
        //        FK_Customer = dataReader["FK_Customer"] == DBNull.Value ? null : Convert.ToInt64(dataReader["FK_Customer"]),
        //        EstimateAmt = dataReader["EstimateAmt"] == DBNull.Value ? null : Convert.ToDecimal(dataReader["EstimateAmt"]),
        //        StartDate = dataReader["StartDate"] == DBNull.Value ? null : Convert.ToDateTime(dataReader["StartDate"]),
        //        EndDate = dataReader["EndDate"] == DBNull.Value ? null : Convert.ToDateTime(dataReader["EndDate"]),
        //        ProjectType = dataReader["ProjectType"] == DBNull.Value ? null : Convert.ToString(dataReader["ProjectType"]),
        //        ProjectStatus = dataReader["ProjectStatus"] == DBNull.Value ? null : Convert.ToString(dataReader["ProjectStatus"]),
        //        CreatedBy = dataReader["CreatedBy"] == DBNull.Value ? null : Convert.ToInt64(dataReader["CreatedBy"]),
        //        CreatedOn = Convert.ToDateTime(dataReader["CreatedOn"]),
        //        UpdatedBy = dataReader["UpdatedBy"] == DBNull.Value ? null : Convert.ToInt64(dataReader["UpdatedBy"]),
        //        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? null : Convert.ToDateTime(dataReader["UpdatedOn"])
        //    };
        //}
    }
}
