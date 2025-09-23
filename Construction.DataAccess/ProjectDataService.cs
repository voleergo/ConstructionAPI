using Construction.Common;
using Construction.DomainModel.Project; // Use the actual namespace
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Construction.DataAccess
{
    public class ProjectDataService
    {
        private readonly string _connectionString;
        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        public ProjectDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ProjectModel> GetProject(ProjectModel inputModel)
        {
            List<ProjectModel> resultList = new List<ProjectModel>();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_GetProject;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@ID_Project", DbType.Int64, inputModel.projectID);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ProjectModel model = new ProjectModel();

                        model.projectID = dataReader["ID_Project"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID_Project"]);
                        model.projectCode = dataReader["ProjectCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["ProjectCode"]);
                        model.projectName = dataReader["ProjectName"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["ProjectName"]);                  
                        model.projectStatus = dataReader["ProjectStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["ProjectStatus"]);
                        model.FK_Customer = dataReader["FK_Customer"] == DBNull.Value ? (int?)null : Convert.ToInt32(dataReader["FK_Customer"]);
                        model.customerCode = dataReader["CustomerCode"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["CustomerCode"]);
                        model.startDate = dataReader["StartDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["StartDate"]);
                        model.endDate = dataReader["EndDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["EndDate"]);
                        model.budget = dataReader["Budget"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dataReader["Budget"]);
                        model.expenses = dataReader["Expense"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dataReader["Expense"]);
                        model.description = dataReader["Description"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["Description"]);
                        model.projectType = dataReader["ProjectType"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["ProjectType"]);
                        model.FK_User = dataReader["FK_User"] == DBNull.Value ? (int?)null : Convert.ToInt32(dataReader["FK_User"]);
                        model.IsActive = dataReader["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsActive"]);
                        model.IsDelete = dataReader["IsDelete"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsDelete"]);
                        model.CreatedBy = dataReader["CreatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(dataReader["CreatedBy"]);
                        model.CreatedOn = dataReader["CreatedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["CreatedOn"]);
                        model.ModifiedBy = dataReader["ModifiedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(dataReader["ModifiedBy"]);
                        model.ModifiedOn = dataReader["ModifiedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ModifiedOn"]);

                        resultList.Add(model);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return resultList;
        }
    }
}