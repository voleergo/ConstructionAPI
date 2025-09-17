/*----------------------------------- UsersDataService  -----------------------------------------------------------------------------------------------------------------------
Purpose    : AuthDataService  Class
Author     : Jinesh Kumar C
Copyright  :
Created on :01-11-2023 09:32:03
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On                          By                    TaskID          Description
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
01-11-2023 09:32:03          Jinesh Kumar C                        AuthDataService initially  created
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using Microsoft.Extensions.Options;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Runtime;
using Construction;
using Construction.DomainModel;
using Construction.DomainModel.User;
using Construction.DomainModel.HttpResponse;
using System.Reflection;
using Construction.DataAccess;
using Newtonsoft.Json;
using Construction.Common;


namespace Construction.DataAccess
{
    public class UserDataService
    {
        private readonly string _connectionString;
        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;
        public UserDataService(string connectionString)
        {
            _connectionString = connectionString;
        }





        public UsersModel ValidateLogin(LoginModel model)
        {
            // Initialize database
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);

            UsersModel userModel = new UsersModel();
            string sqlCommand = Procedures.SP_ValidateLogin; // Your SP constant
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            // Add parameters for SP
            db.AddInParameter(dbCommand, "@UserInput", DbType.String, model.UserInput); // Email or Mobile
            db.AddInParameter(dbCommand, "@Password", DbType.String, model.Password);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        userModel.ID_Users = Convert.ToInt64(dataReader["ID_Users"]);
                        userModel.UserName = Convert.ToString(dataReader["UserName"]);
                        userModel.Email = Convert.ToString(dataReader["Email"]);
                        userModel.MobileNumber = Convert.ToString(dataReader["MobileNumber"]);
                        userModel.FK_UserRole = Convert.ToInt16(dataReader["FK_Role"]);
                        userModel.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                        userModel.UserStatus = Convert.ToString(dataReader["UserStatus"]);
                        userModel.CreatedOn = dataReader["CreatedOn"] as DateTime?;
                        userModel.CreatedBy = dataReader["CreatedBy"] as string;
                        userModel.ModifiedOn = dataReader["ModifiedOn"] as DateTime?;
                        userModel.ModifiedBy = dataReader["ModifiedBy"] as string;
                        userModel.LastPasswordChangeDate = dataReader["LastPasswordChangeDate"] as DateTime?;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while validating user login.", ex);
            }

            return userModel;
        }





        public HttpResponses UsersDelete(UsersModel inputModel)
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            HttpResponses response = new HttpResponses();
            string sqlCommand = Procedures.SP_UsersDelete;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int64, inputModel.ID_Users);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        response.ResponseCode = Convert.ToString(dataReader["ResponseCode"]);
                        response.ResponseMessage = Convert.ToString(dataReader["ResponseMessage"]);
                        response.ResponseStatus = Convert.ToBoolean(dataReader["ResponseStatus"]);
                        response.ResponseID = Convert.ToInt64(dataReader["ResponseID"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return response;
        }



        public List<UsersModel> GetUsers(UsersModel inputModel)
        {
            List<UsersModel> resultList = new List<UsersModel>();
            UsersModel model = new UsersModel();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_GetUsers;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int64, inputModel.ID_Users);
            //db.AddInParameter(dbCommand, "@CreatedBy", DbType.String, inputModel.CreatedBy);
            //db.AddInParameter(dbCommand, "@SearchField", DbType.String, inputModel.SearchField);
            //db.AddInParameter(dbCommand, "@SearchValue", DbType.String, inputModel.SearchValue);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        model = new UsersModel();
                        model.ID_Users = Convert.ToInt32(dataReader["ID_Users"]);
                        model.UserName = Convert.ToString(dataReader["UserName"]);
                        model.Password = Convert.ToString(dataReader["UserPassword"]);
                        model.MobileNumber = Convert.ToString(dataReader["MobileNumber"]);
                        model.Email = Convert.ToString(dataReader["Email"]);
                        resultList.Add(model);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return resultList;
        }


        public HttpResponses UsersUpdate(UsersModel inputModel)
        {
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            SignUpResponse response = new SignUpResponse();
            string sqlCommand = Procedures.SP_UpdateUser;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int32, inputModel.ID_Users);
            //db.AddInParameter(dbCommand, "@UserName", DbType.String, inputModel.UserName);
            db.AddInParameter(dbCommand, "@UserPassword", DbType.String, inputModel.Password);
            db.AddInParameter(dbCommand, "@UserName", DbType.String, inputModel.UserName);
            db.AddInParameter(dbCommand, "@Email", DbType.String, inputModel.Email);
            db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, inputModel.MobileNumber);
            //db.AddInParameter(dbCommand, "@FK_State", DbType.Int32, inputModel.FK_State);
            //db.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, inputModel.CreatedBy);
            //db.AddInParameter(dbCommand, "@ModifiedBy", DbType.Int32, inputModel.ModifiedBy);
            //db.AddInParameter(dbCommand, "@IPAddress", DbType.String, inputModel.IPAddress);
            //db.AddInParameter(dbCommand, "@MACAddress", DbType.String, inputModel.MACAddress);
            //db.AddInParameter(dbCommand, "@FK_UserImages", DbType.Int64, inputModel.FK_UserImages);


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        response.ResponseCode = Convert.ToString(dataReader["ResponseCode"]);
                        response.ResponseMessage = Convert.ToString(dataReader["ResponseMessage"]);
                        response.ResponseStatus = Convert.ToBoolean(dataReader["ResponseStatus"]);
                        response.ResponseID = Convert.ToInt64(dataReader["ResponseID"]);
                        // response.RegID = Convert.ToString(dataReader["RegID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

       







        public string GetTenantData()
        {
            string result = string.Empty;
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_GetTenantData; // "GetTenantData"
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        // concatenate rows into JSON string
                        result += Convert.ToString(dataReader[0]);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        


        public HttpResponses UsersDataUpdate(UsersModel inputModel)
        {
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            HttpResponses response = new HttpResponses();
            string sqlCommand = Procedures.SP_UpdateUserProfile;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@ID_UserProfile", DbType.Int32, inputModel.ID_UserProfile);
            db.AddInParameter(dbCommand, "@FK_Users", DbType.Int32, inputModel.FK_Users);
            db.AddInParameter(dbCommand, "@FK_Gender", DbType.Int32, inputModel.FK_Gender);
            db.AddInParameter(dbCommand, "@FK_Profession", DbType.Int32, inputModel.FK_Profession);
            db.AddInParameter(dbCommand, "@FK_IdentityProofType", DbType.Int32, inputModel.FK_IdentityProofType);
            db.AddInParameter(dbCommand, "@FK_State", DbType.Int32, inputModel.FK_State);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int32, inputModel.FK_Country);
            //db.AddInParameter(dbCommand, "@IsStudent", DbType.Boolean, inputModel.IsStudent);
            //db.AddInParameter(dbCommand, "@CompanyName", DbType.String, inputModel.FK_Company);
            db.AddInParameter(dbCommand, "@CollegeName", DbType.String, inputModel.CollegeName);
            db.AddInParameter(dbCommand, "@ProofNumber", DbType.String, inputModel.ProofNumber);
            db.AddInParameter(dbCommand, "@Address1", DbType.String, inputModel.Address1);
            db.AddInParameter(dbCommand, "@Address2", DbType.String, inputModel.Address2);
            db.AddInParameter(dbCommand, "@City", DbType.String, inputModel.City);
            db.AddInParameter(dbCommand, "@PostalCode", DbType.String, inputModel.PostalCode);
            db.AddInParameter(dbCommand, "FK_UserImages", DbType.Int32, inputModel.FK_UserImages);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        response.ResponseCode = Convert.ToString(dataReader["ResponseCode"]);
                        response.ResponseMessage = Convert.ToString(dataReader["ResponseMessage"]);
                        response.ResponseStatus = Convert.ToBoolean(dataReader["ResponseStatus"]);
                        response.ResponseID = Convert.ToInt64(dataReader["ResponseID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }










       

        public List<UserDataUpdate> UsersUpdateSelect(Int64 FK_Users)
        {
            List<UserDataUpdate> resultList = new List<UserDataUpdate>();
            UserDataUpdate model = new UserDataUpdate();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_SelectUserProfile;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@FK_Users ", DbType.Int64, FK_Users);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        model = new UserDataUpdate();
                        model.ID_UserProfile = Convert.ToInt32(dataReader["ID_UserProfile"]);
                        model.FK_Users = Convert.ToInt32(dataReader["FK_Users"]);
                        model.FK_Gender = Convert.ToInt32(dataReader["FK_Gender"]);
                        model.FK_Profession = Convert.ToInt32(dataReader["FK_Profession"]);
                        model.FK_UserImages = Convert.ToInt32(dataReader["FK_UserImages"]);
                        model.ImageURL = Convert.ToString(dataReader["ImageURL"]);
                        model.CollegeName = Convert.ToString(dataReader["CollegeName"]);
                        model.FK_IdentityProofType = Convert.ToInt32(dataReader["FK_IdentityProofType"]);
                        model.FK_State = Convert.ToInt32(dataReader["FK_State"]);
                        model.FK_Country = Convert.ToInt32(dataReader["FK_Country"]);
                        model.FK_Company = Convert.ToString(dataReader["CompanyName"]);
                        model.ProofNumber = Convert.ToString(dataReader["ProofNumber"]);
                        //model.IsCompany = Convert.ToBoolean(dataReader["IsCompany"]);
                        model.Address1 = Convert.ToString(dataReader["Address1"]);
                        //model.IsStudent = Convert.ToBoolean(dataReader["IsStudent"]);
                        model.Address2 = Convert.ToString(dataReader["Address2"]);
                        model.City = Convert.ToString(dataReader["City"]);
                        model.PostalCode = Convert.ToString(dataReader["PostalCode"]);
                        model.CreatedOn = Convert.ToDateTime(dataReader["CreatedOn"]);
                        model.ModifiedOn = Convert.ToDateTime(dataReader["ModifiedOn"]);
                        model.FullName = Convert.ToString(dataReader["FullName"]);
                        model.Email = Convert.ToString(dataReader["Email"]);
                        model.MobileNumber = Convert.ToString(dataReader["MobileNumber"]);
                        model.GenderName = Convert.ToString(dataReader["GenderName"]);
                        model.ProfessionName = Convert.ToString(dataReader["ProfessionName"]);
                        model.ProofTypeName = Convert.ToString(dataReader["ProofTypeName"]);
                        model.StateName = Convert.ToString(dataReader["StateName"]);
                        model.CountryName = Convert.ToString(dataReader["CountryName"]);
                        //model.CompanyName = Convert.ToString(dataReader["CompanyName"]);

                        resultList.Add(model);
                    }
                }

            }
            catch (Exception e)
            {
                throw;
            }
            return resultList;
        }
       




        public HttpResponses DropDownData()
        {
            HttpResponses result = new HttpResponses();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_DropDownData;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        result.ResponseStatus = true;
                        result.ResponseData = Convert.ToString(dataReader["Data"]);
                    }
                }
            }
            catch (Exception ex)
            {
                result.ResponseStatus = false;
                result.ResponseData = string.Empty;
            }
            return result;
        }
        public HttpResponses CommonDropDownData()
        {
            HttpResponses result = new HttpResponses();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_SelectDropDownData;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        result.ResponseStatus = true;
                        result.ResponseData = Convert.ToString(dataReader["Data"]);
                    }
                }
            }
            catch (Exception ex)
            {
                result.ResponseStatus = false;
                result.ResponseData = string.Empty;
            }
            return result;
        }
        //public HttpResponses AdminLogin(UsersModel model)
        //{
        //    HttpResponses result = new HttpResponses();
        //    DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
        //    Database db = EnterpriseExtentions.GetDatabase(_connectionString);
        //    string sqlCommand = Procedures.SP_ValidateLogin;
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    dbCommand.CommandTimeout = 0;
        //    db.AddInParameter(dbCommand, "@UserCode", DbType.String, model.UserName);
        //    db.AddInParameter(dbCommand, "@Password", DbType.String, model.Password);
        //    db.AddInParameter(dbCommand, "@IP", DbType.String, model.IPAddress);
        //    db.AddInParameter(dbCommand, "@MAC", DbType.String, model.MACAddress);
        //    db.AddInParameter(dbCommand, "@IsMobile", DbType.Boolean, model.IsMobile);
        //    try
        //    {
        //        using (IDataReader dataReader = db.ExecuteReader(dbCommand))
        //        {
        //            while (dataReader.Read())
        //            {
        //                result.ResponseCode = Convert.ToString(dataReader["ResponseCode"]);
        //                result.ResponseMessage = Convert.ToString(dataReader["ResponseMessage"]);
        //                result.ResponseStatus = Convert.ToBoolean(dataReader["ResponseStatus"]);
        //                result.ResponseID = Convert.ToInt64(dataReader["ResponseID"]);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //    return result;
        //}


        public List<ClientMenuModel> GetMenuClient(ClientMenuModel inputmodel)
        {
            List<ClientMenuModel> clients = new List<ClientMenuModel>();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = "usp_SelectMenuClient";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            db.AddInParameter(dbCommand, "@FK_Tenant", DbType.Int32, inputmodel.FK_Tenant);
            db.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, inputmodel.CreatedBy); // ✅ fixed type
            db.AddInParameter(dbCommand, "@ID_MenuClient", DbType.Int32, inputmodel.ID_MenuClient);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var client = new ClientMenuModel();

                    client.ID_MenuClient = dataReader["ID_MenuClient"] != DBNull.Value
                        ? Convert.ToInt32(dataReader["ID_MenuClient"])
                        : 0;

                    client.FK_Tenant = dataReader["FK_Tenant"] != DBNull.Value
                        ? Convert.ToInt32(dataReader["FK_Tenant"])
                        : 0;

                    client.FK_Menu = dataReader["FK_Menu"] != DBNull.Value
                        ? Convert.ToInt32(dataReader["FK_Menu"])
                        : 0;

                    client.MenuName = dataReader["MenuName"] != DBNull.Value
                        ? Convert.ToString(dataReader["MenuName"])
                        : string.Empty;

                    client.UserMenu = dataReader["UserMenu"] != DBNull.Value
                        ? Convert.ToString(dataReader["UserMenu"])
                        : string.Empty;

                    client.CreatedBy = dataReader["CreatedBy"] != DBNull.Value
                        ? Convert.ToInt32(dataReader["CreatedBy"])
                        : 0;

                    client.CreatedDate = dataReader["CreatedDate"] != DBNull.Value
                        ? Convert.ToDateTime(dataReader["CreatedDate"])
                        : (DateTime?)null;

                    client.ModifiedBy = dataReader["ModifiedBy"] != DBNull.Value
                        ? Convert.ToInt32(dataReader["ModifiedBy"])
                        : 0;

                    client.ModifiedDate = dataReader["ModifiedDate"] != DBNull.Value
                        ? Convert.ToDateTime(dataReader["ModifiedDate"])
                        : (DateTime?)null;

                    client.IsActive = dataReader["IsActive"] != DBNull.Value
                        ? Convert.ToBoolean(dataReader["IsActive"])
                        : false;

                    client.IsDelete = dataReader["IsDelete"] != DBNull.Value
                        ? Convert.ToBoolean(dataReader["IsDelete"])
                        : false;

                    clients.Add(client);
                }
            }
            return clients;
        }

        public HttpResponses UpdateMenuClient(ClientMenuModel client)
        {
            HttpResponses result = new HttpResponses();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);

            string sqlCommand = Procedures.SP_InsertMenuClient; // usp_InsertMenuClient
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            // 🔹 Build JSON payload if not already provided
            string jsonData = client.JsonData;
            if (string.IsNullOrEmpty(client.JsonData))
            {
                var payload = new[] {
        new {
            iD_MenuClient = client.ID_MenuClient,
            fK_Menu = client.FK_Menu,
            fK_Tenant = client.FK_Tenant,
            userMenu = client.UserMenu,
            menuName = client.MenuName,
            isActive = client.IsActive,
            isDelete = client.IsDelete,
            createdBy = client.CreatedBy,
            menuParent = client.MenuParent
        }
    };
                client.JsonData = JsonConvert.SerializeObject(payload);
            }


            db.AddInParameter(dbCommand, "@JsonData", DbType.String, jsonData);
            db.AddInParameter(dbCommand, "@FK_Tenant", DbType.Int32, client.FK_Tenant);

            try
            {
                using (IDataReader datareader = db.ExecuteReader(dbCommand))
                {
                    while (datareader.Read())
                    {
                        result.ResponseMessage = Convert.ToString(datareader["ResponseMessage"]);
                        result.ResponseCode = Convert.ToString(datareader["ResponseCode"]);
                        result.ResponseStatus = Convert.ToBoolean(datareader["ResponseStatus"]);
                        result.ResponseID = Convert.ToInt32(datareader["ResponseID"]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error executing usp_InsertMenuClient", e);
            }

            return result;
        }


        public List<MenuRoleModel> GetMenuRoleModel(MenuRoleModel menu)
        {
            List<MenuRoleModel> menurole = new List<MenuRoleModel>();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_SelectMenuRole;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            db.AddInParameter(dbCommand, "@FK_Role", DbType.Int32, menu.FK_Role);
            db.AddInParameter(dbCommand, "@FK_Tenant", DbType.Int32, menu.FK_Tenant);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var menus = new MenuRoleModel();

                    // From MenuRole
                    menus.ID_Menufilter = dataReader["ID_MenuRole"] != DBNull.Value
                        ? Convert.ToInt32(dataReader["ID_MenuRole"])
                        : 0;

                    menus.FK_MenuClient = dataReader["FK_MenuClient"] != DBNull.Value
                        ? Convert.ToInt32(dataReader["FK_MenuClient"])
                        : 0;

                    menus.MenuRole = Convert.ToString(dataReader["MenuRole"]);

                    menus.IsAdd = dataReader["IsAdd"] != DBNull.Value && Convert.ToBoolean(dataReader["IsAdd"]);
                    menus.IsEdit = dataReader["IsEdit"] != DBNull.Value && Convert.ToBoolean(dataReader["IsEdit"]);
                    menus.IsDelete = dataReader["IsDelete"] != DBNull.Value && Convert.ToBoolean(dataReader["IsDelete"]);
                    menus.IsView = dataReader["IsView"] != DBNull.Value && Convert.ToBoolean(dataReader["IsView"]);
                    menus.IsPrint = dataReader["IsPrint"] != DBNull.Value && Convert.ToBoolean(dataReader["IsPrint"]);
                    menus.Active = dataReader["IsActive"] != DBNull.Value && Convert.ToBoolean(dataReader["IsActive"]);

                    // From MenuClient
                    menus.FK_Menu = dataReader["FK_Menu"] != DBNull.Value
                        ? Convert.ToInt32(dataReader["FK_Menu"])
                        : 0;

                    menus.MenuName = Convert.ToString(dataReader["UserMenu"]);

                    // Extra property (if your model needs it)
                    menus.Header = 0;

                    menurole.Add(menus);
                }
            }
            return menurole;
        }


        //public HttpResponses RoleModelUpdate(RoleModel userRole)
        //{
        //    Database db = EnterpriseExtentions.GetDatabase(_connectionString);
        //    HttpResponses responses = new HttpResponses();
        //    String sqlCommand = Procedures.SP_db_Role;
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    dbCommand.CommandTimeout = 0;
        //    db.AddInParameter(dbCommand, "@ID", DbType.Int32, userRole.ID);
        //    db.AddInParameter(dbCommand, "@Name", DbType.String, userRole.Name);
        //    db.AddInParameter(dbCommand, "@ModifiedBy", DbType.Int32, userRole.ModifiedBy);
        //    db.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, userRole.CreatedBy);

        //   try
        //    {
        //        using (IDataReader dataReader = db.ExecuteReader(dbCommand))
        //        {
        //            while (dataReader.Read())
        //            {
        //                responses.ResponseMessage = Convert.ToString(dataReader["ResponseMessage"]);

        //            }
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        throw;
        //    }
        //    return responses;
        //}


        //public HttpResponses userRoleDelete(RoleModel userRoleModel)
        //{
        //    DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
        //    Database db = EnterpriseExtentions.GetDatabase(_connectionString);
        //    HttpResponses response = new HttpResponses();
        //    string sqlCommand = Procedures.SP_db_DeleteRole;
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    dbCommand.CommandTimeout = 0;
        //    db.AddInParameter(dbCommand, "@ID", DbType.Int32, userRoleModel.ID);

        //    try
        //    {
        //        using (IDataReader dataReader = db.ExecuteReader(dbCommand)) 
        //        {
        //            while (dataReader.Read())
        //            {

        //                response.ResponseMessage = Convert.ToString(dataReader["ResponseMessage"]);

        //            }
        //        }
        //    }
        //    catch( Exception e)
        //    {
        //        throw;
        //    }
        //    return response;
        //}

        //public List<RoleModel> getUserRole(RoleModel userRole)
        //{
        //    List<RoleModel> menuRoleModel = new List<RoleModel>();
        //    DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
        //    Database db = EnterpriseExtentions.GetDatabase(_connectionString);
        //    string sqlCommand = Procedures.SP_db_GetUserRole;
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    dbCommand.CommandTimeout = 0;
        //    db.AddInParameter(dbCommand, "@ID", DbType.Int32, userRole.ID);
        //    //db.AddInParameter(dbCommand, "@Name", DbType.String, userRole.Name);
        //    //db.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, userRole.CreatedBy);
        //    //db.AddInParameter(dbCommand, "@ModifiedBy", DbType.Int32, userRole.ModifiedBy);
        //    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
        //    {
        //        while (dataReader.Read())
        //        {
        //            var UserData = new RoleModel();
        //            UserData.ID = Convert.ToInt32(dataReader["ID"]);
        //            UserData.Name = Convert.ToString(dataReader["Name"]);
        //            UserData.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
        //            UserData.ModifiedBy = Convert.ToInt32(dataReader["ModifiedBy"]);
        //            menuRoleModel.Add(UserData);
        //        }

        //    }
        //    return menuRoleModel;
        //}

        public HttpResponses PostMenuModel(MenuModel model)
        {
            // Convert MenuModel to JSON internally
            var jsonData = JsonConvert.SerializeObject(new
            {
                fK_Pages = model.FK_Pages,
                moduleCode = model.ModuleCode,
                menuName = model.MenuName,
                menuParent = model.MenuParent,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy
            });

            HttpResponses result = new HttpResponses();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = "usp_InsertMenuData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@JsonData", DbType.String, jsonData);

            try
            {
                using (IDataReader datareader = db.ExecuteReader(dbCommand))
                {
                    while (datareader.Read())
                    {
                        result.ResponseMessage = Convert.ToString(datareader["ResponseMessage"]);
                        result.ResponseCode = Convert.ToString(datareader["ResponseCode"]);
                        result.ResponseStatus = Convert.ToBoolean(datareader["ResponseStatus"]);
                    }
                }
            }
            catch (Exception ex)
            {
                result.ResponseCode = "0";
                result.ResponseMessage = ex.Message;
                result.ResponseStatus = false;
            }
            return result;
        }
        public HttpResponses UpdateMenuRole(MenuRoleModel menu)
        {
            HttpResponses result = new HttpResponses();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.Sp_UpdateMenuRole;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            // Only pass the parameter the SP expects
            db.AddInParameter(dbCommand, "@JsonData", DbType.String, menu.JsonData);

            try
            {
                using (IDataReader datareader = db.ExecuteReader(dbCommand))
                {
                    while (datareader.Read())
                    {
                        result.ResponseMessage = Convert.ToString(datareader["ResponseMessage"]);
                        result.ResponseCode = Convert.ToString(datareader["ResponseCode"]);
                        result.ResponseStatus = Convert.ToBoolean(datareader["ResponseStatus"]);
                        result.ResponseID = datareader["ResponseID"] != DBNull.Value
                                                ? Convert.ToInt32(datareader["ResponseID"])
                                                : 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error executing usp_UpdateMenuRole", e);
            }
            return result;
        }

        public List<RoleModel> GetRoles(int idRole)
        {
            List<RoleModel> roles = new List<RoleModel>();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_GetRoles;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Role", DbType.Int32, idRole);

            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    RoleModel role = new RoleModel
                    {
                        ID_Role = Convert.ToInt32(reader["ID_Role"]),
                        Roles = Convert.ToString(reader["Roles"]),
                        CreatedBy = Convert.ToString(reader["CreatedBy"]),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? null : Convert.ToString(reader["ModifiedBy"]),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"])
                    };
                    roles.Add(role);
                }
            }
            return roles;
        }

        public List<MenuModel> GetMenuModel(MenuModel menu)
        {
            List<MenuModel> menuModels = new List<MenuModel>();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_SelectMenuData;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@ID_Menu", DbType.Int32, menu.ID_Menu); // Changed from FK_Menu to ID_Menu

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var menus = new MenuModel();
                    menus.ID_Menu = Convert.ToInt32(dataReader["ID_Menu"]); // Changed from FK_Menu to ID_Menu
                    menus.FK_Pages = Convert.ToInt32(dataReader["FK_Pages"]);
                    menus.ModuleCode = Convert.ToString(dataReader["ModuleCode"]);
                    menus.MenuName = Convert.ToString(dataReader["MenuName"]);
                    menus.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                    menus.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                    menus.ModifiedBy = dataReader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(dataReader["ModifiedBy"]) : (int?)null;
                    menus.ModifiedDate = dataReader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(dataReader["ModifiedDate"]) : (DateTime?)null;

                    menuModels.Add(menus);
                }
            }
            return menuModels;
        }
        public HttpResponses MenuDelete(MenuModel model)
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            HttpResponses response = new HttpResponses();
            string sqlCommand = "usp_DeleteMenuData"; // Updated to match your procedure name
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@ID_Menu", DbType.Int32, model.ID_Menu); // Changed from FK_Menu to ID_Menu and DbType.Int64 to DbType.Int32

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        response.ResponseCode = Convert.ToString(dataReader["ResponseCode"]);
                        response.ResponseMessage = Convert.ToString(dataReader["ResponseMessage"]);
                        response.ResponseStatus = Convert.ToBoolean(dataReader["ResponseStatus"]);
                        // Note: Your stored procedure doesn't return ResponseID, so remove this line
                        // response.ResponseID = Convert.ToInt64(dataReader["ResponseID"]);
                    }
                }
            }
            catch (Exception e)
            {
                // Add proper error handling instead of just throwing
                response.ResponseCode = "0";
                response.ResponseMessage = e.Message;
                response.ResponseStatus = false;
            }
            return response;
        }

        public HttpResponses PostMenuModel(string jsonData)
        {
            throw new NotImplementedException();
        }
    }
}
