using Construction.DomainModel;
using Construction.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Construction.DataAccess
{
    public class ProjectTransRepository : BaseRepository<ProjectTrans>, IProjectTransRepository
    {
        protected override string TableName => "ProjectTrans";
        protected override string IdColumn => "ID_ProjectTrans";

        public ProjectTransRepository(DatabaseConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public override async Task<IEnumerable<ProjectTrans>> GetAllAsync()
        {
            return await GetMultipleStoredProcAsync("usp_ProjectTrans_GetAll");
        }

        public override async Task<ProjectTrans?> GetByIdAsync(long id)
        {
            return await GetSingleStoredProcAsync("usp_ProjectTrans_GetById", new { ID_ProjectTrans = id });
        }

        public override async Task<long> AddAsync(ProjectTrans projectTrans)
        {
            return await ExecuteInsertStoredProcAsync("usp_ProjectTrans_Insert", new
            {
                projectTrans.FK_Project,
                projectTrans.FK_Level,
                projectTrans.FK_Item,
                projectTrans.AccountType,
                projectTrans.Amount,
                projectTrans.Qty,
                projectTrans.Description,
                projectTrans.CreatedOn,
                projectTrans.CreatedBy,
                projectTrans.ModifiedOn,
                projectTrans.ModifiedBy
            });
        }

        public override async Task<bool> UpdateAsync(ProjectTrans projectTrans)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_ProjectTrans_Update", new
            {
                projectTrans.ID_ProjectTrans,
                projectTrans.FK_Project,
                projectTrans.FK_Level,
                projectTrans.FK_Item,
                projectTrans.AccountType,
                projectTrans.Amount,
                projectTrans.Qty,
                projectTrans.Description,
                projectTrans.ModifiedOn,
                projectTrans.ModifiedBy
            });
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            return await ExecuteNonQueryStoredProcAsync("usp_ProjectTrans_Delete", new { ID_ProjectTrans = id });
        }

        public async Task<IEnumerable<ProjectTrans>> GetByProjectIdAsync(long projectId)
        {
            return await GetMultipleStoredProcAsync("usp_ProjectTrans_GetByProject", new { FK_Project = projectId });
        }

        public async Task<IEnumerable<ProjectTrans>> GetByLevelIdAsync(long levelId)
        {
            return await GetMultipleStoredProcAsync("usp_ProjectTrans_GetByLevel", new { FK_Level = levelId });
        }

        public async Task<IEnumerable<ProjectTrans>> GetByItemIdAsync(long itemId)
        {
            return await GetMultipleStoredProcAsync("usp_ProjectTrans_GetByItem", new { FK_Item = itemId });
        }

        public async Task<IEnumerable<ProjectTrans>> GetByAccountTypeAsync(string accountType)
        {
            return await GetMultipleStoredProcAsync("usp_ProjectTrans_GetByAccountType", new { AccountType = accountType });
        }

        protected override ProjectTrans MapFromReader(IDataReader dataReader)
        {
            return new ProjectTrans
            {
                ID_ProjectTrans = Convert.ToInt64(dataReader["ID_ProjectTrans"]),
                FK_Project = Convert.ToInt64(dataReader["FK_Project"]),
                FK_Level = Convert.ToInt64(dataReader["FK_Level"]),
                FK_Item = Convert.ToInt64(dataReader["FK_Item"]),
                AccountType = dataReader["AccountType"] == DBNull.Value ? null : Convert.ToString(dataReader["AccountType"]),
                Amount = dataReader["Amount"] == DBNull.Value ? null : Convert.ToDecimal(dataReader["Amount"]),
                Qty = dataReader["Qty"] == DBNull.Value ? null : Convert.ToDecimal(dataReader["Qty"]),
                Description = dataReader["Description"] == DBNull.Value ? null : Convert.ToString(dataReader["Description"]),
                CreatedOn = Convert.ToDateTime(dataReader["CreatedOn"]),
                CreatedBy = dataReader["CreatedBy"] == DBNull.Value ? null : Convert.ToInt64(dataReader["CreatedBy"]),
                ModifiedOn = dataReader["ModifiedOn"] == DBNull.Value ? null : Convert.ToDateTime(dataReader["ModifiedOn"]),
                ModifiedBy = dataReader["ModifiedBy"] == DBNull.Value ? null : Convert.ToInt64(dataReader["ModifiedBy"])
            };
        }
    }
}
