using Construction.Common;
using Construction.DomainModel;
using Construction.DomainModel.Item; // Use the actual namespace
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Construction.DataAccess
{
    public class ItemDataService
    {
        private readonly string _connectionString;
        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        public ItemDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

       
        public HttpResponses DeleteProjectService(int idProjectService)
        {
            HttpResponses response = new HttpResponses();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_DeleteProjectService; 
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            db.AddInParameter(dbCommand, "@ID_ProjectService", DbType.Int32, idProjectService);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        response.ResponseCode = dataReader["ResponseCode"] != DBNull.Value ? Convert.ToString(dataReader["ResponseCode"]) : "0";
                        response.ResponseMessage = dataReader["ResponseMessage"] != DBNull.Value ? Convert.ToString(dataReader["ResponseMessage"]) : string.Empty;
                        response.ResponseStatus = dataReader["ResponseStatus"] != DBNull.Value && Convert.ToBoolean(dataReader["ResponseStatus"]);
                        response.ResponseID = dataReader["ResponseID"] != DBNull.Value ? Convert.ToInt64(dataReader["ResponseID"]) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = "0";
                response.ResponseMessage = ex.Message;
                response.ResponseStatus = false;
            }

            return response;
        }
        public List<ProjectServiceModel> GetProjectServices(ProjectServiceModel service)
        {
            List<ProjectServiceModel> services = new List<ProjectServiceModel>();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_GetProjectService;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int32, service.FK_Project);
            db.AddInParameter(dbCommand, "@ID_ProjectService", DbType.Int32, service.ID_ProjectService);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    ProjectServiceModel s = new ProjectServiceModel
                    {
                        ID_ProjectService = dataReader["ID_ProjectService"] != DBNull.Value ? Convert.ToInt32(dataReader["ID_ProjectService"]) : 0,
                        FK_ServiceCategory = dataReader["FK_ServiceCategory"] != DBNull.Value ? Convert.ToInt32(dataReader["FK_ServiceCategory"]) : 0,
                        ProjectService = dataReader["ProjectService"] != DBNull.Value ? Convert.ToString(dataReader["ProjectService"]) : string.Empty,
                        FK_Project = dataReader["FK_Project"] != DBNull.Value ? Convert.ToInt32(dataReader["FK_Project"]) : 0,
                        Quantity = dataReader["Quantity"] != DBNull.Value ? Convert.ToInt32(dataReader["Quantity"]) : 0,
                        Unit = dataReader["Unit"] != DBNull.Value ? Convert.ToString(dataReader["Unit"]) : string.Empty,
                        UnitPrice = dataReader["UnitPrice"] != DBNull.Value ? Convert.ToDecimal(dataReader["UnitPrice"]) : 0,
                        TotalPrice = dataReader["TotalPrice"] != DBNull.Value ? Convert.ToDecimal(dataReader["TotalPrice"]) : 0,
                        FK_Supplier = dataReader["FK_Supplier"] != DBNull.Value ? Convert.ToInt32(dataReader["FK_Supplier"]) : 0
                    };
                    services.Add(s);
                }
            }

            return services;
        }

        public HttpResponses UpdateProjectService(ProjectServiceModel service)
        {
            HttpResponses response = new HttpResponses();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_UpdateProjectService;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            // Add input parameters
            db.AddInParameter(dbCommand, "@ID_ProjectService", DbType.Int32, service.ID_ProjectService);
            db.AddInParameter(dbCommand, "@FK_ServiceCategory", DbType.Int32, service.FK_ServiceCategory);
            db.AddInParameter(dbCommand, "@ProjectService", DbType.String, service.ProjectService);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int64, service.FK_Project);
            db.AddInParameter(dbCommand, "@Unit", DbType.String, service.Unit);
            db.AddInParameter(dbCommand, "@Quantity", DbType.Int64, service.Quantity);
            db.AddInParameter(dbCommand, "@UnitPrice", DbType.Decimal, service.UnitPrice);
            db.AddInParameter(dbCommand, "@TotalPrice", DbType.Decimal, service.TotalPrice);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, service.FK_Supplier);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        response.ResponseCode = dataReader["ResponseCode"] != DBNull.Value ? Convert.ToString(dataReader["ResponseCode"]) : "0";
                        response.ResponseMessage = dataReader["ResponseMessage"] != DBNull.Value ? Convert.ToString(dataReader["ResponseMessage"]) : string.Empty;
                        response.ResponseStatus = dataReader["ResponseStatus"] != DBNull.Value && Convert.ToBoolean(dataReader["ResponseStatus"]);
                        response.ResponseID = dataReader["ResponseID"] != DBNull.Value ? Convert.ToInt64(dataReader["ResponseID"]) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = "0";
                response.ResponseMessage = ex.Message;
                response.ResponseStatus = false;
            }

            return response;
        }
        public List<SupplierModel> GetSuppliers(SupplierModel supplier)
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_GetSupplier;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            db.AddInParameter(dbCommand, "@ID_Supplier", DbType.Int32, supplier.ID_Supplier);
            db.AddInParameter(dbCommand, "@FK_ServiceCategory", DbType.Int32, supplier.FK_ServiceCategory);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SupplierModel s = new SupplierModel
                    {
                        ID_Supplier = dataReader["ID_Supplier"] != DBNull.Value ? Convert.ToInt32(dataReader["ID_Supplier"]) : 0,
                        SupplierCode = dataReader["SupplierCode"] != DBNull.Value ? Convert.ToString(dataReader["SupplierCode"]) : string.Empty,
                        SupplierName = dataReader["SupplierName"] != DBNull.Value ? Convert.ToString(dataReader["SupplierName"]) : string.Empty,
                        CreatedOn = dataReader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(dataReader["CreatedOn"]) : DateTime.MinValue,
                        CreatedBy = dataReader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(dataReader["CreatedBy"]) : 0,
                        ModifiedOn = dataReader["ModifiedOn"] != DBNull.Value ? Convert.ToDateTime(dataReader["ModifiedOn"]) : null,
                        ModifiedBy = dataReader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(dataReader["ModifiedBy"]) : null,
                        FK_ServiceCategory = dataReader["FK_ServiceCategory"] != DBNull.Value ? Convert.ToInt32(dataReader["FK_ServiceCategory"]) : 0,
                        CategoryName = dataReader["CategoryName"] != DBNull.Value ? Convert.ToString(dataReader["CategoryName"]) : string.Empty
                    };
                    suppliers.Add(s);
                }
            }

            return suppliers;
        }


        public List<Item> GetServiceCategory(Item data)
        {
            List<Item> resultList = new List<Item>();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_GetServiceCategory;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            db.AddInParameter(dbCommand, "@ID_ServiceCategory", DbType.Int64, data.ID_ServiceCategory);
            db.AddInParameter(dbCommand, "@FK_ProjectType", DbType.Int64, data.FK_ProjectType);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        Item item = new Item
                        {
                            ID_ServiceCategory = Convert.ToInt32(dataReader["ID_ServiceCategory"]),
                            CategoryName = dataReader["CategoryName"] == DBNull.Value
                                           ? string.Empty
                                           : Convert.ToString(dataReader["CategoryName"]),
                            FK_ProjectType = Convert.ToInt32(dataReader["FK_ProjectType"])
                        };

                        resultList.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return resultList;
        }



        public HttpResponses UpdateServiceCategory (Item service)
        {
            HttpResponses response = new HttpResponses();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_UpdateServiceCategory;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            // Add input parameters
            db.AddInParameter(dbCommand, "@ID_ServiceCategory", DbType.Int32, service.ID_ServiceCategory);
            db.AddInParameter(dbCommand, "CategoryName", DbType.String, service.CategoryName);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        response.ResponseCode = dataReader["ResponseCode"] != DBNull.Value ? Convert.ToString(dataReader["ResponseCode"]) : "0";
                        response.ResponseMessage = dataReader["ResponseMessage"] != DBNull.Value ? Convert.ToString(dataReader["ResponseMessage"]) : string.Empty;
                        response.ResponseStatus = dataReader["ResponseStatus"] != DBNull.Value && Convert.ToBoolean(dataReader["ResponseStatus"]);
                        response.ResponseID = dataReader["ResponseID"] != DBNull.Value ? Convert.ToInt64(dataReader["ResponseID"]) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = "0";
                response.ResponseMessage = ex.Message;
                response.ResponseStatus = false;
            }

            return response;
        }

    }
}
