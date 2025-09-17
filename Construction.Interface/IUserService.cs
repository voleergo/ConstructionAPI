/*----------------------------------- UsersDataService  -----------------------------------------------------------------------------------------------------------------------
Purpose    : AuthService Interface Class
Author     : Jinesh Kumar C
Copyright  :
Created on :01-11-2023 09:32:03
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On                          By                    TaskID          Description
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
01-11-2023 09:32:03          Jinesh Kumar C                       AuthService Interface Class initially  created
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System.Reflection;
using Construction.DomainModel;
using Construction.DomainModel.HttpResponse;
using Construction.DomainModel.User;

namespace Construction.Interface
{
    public interface IUserService
    {
        public string? ConnectionStrings { get; set; }

        #region Users
        UsersModel ValidateLogin(LoginModel model); 
        
        HttpResponses UserDataUpdate(UsersModel inputModel);

        List<UsersModel> GetUsers(UsersModel data);
        public HttpResponses UsersUpdate(UsersModel inputModel);

        HttpResponses UsersDelete(UsersModel inputModel);


        string GenerateRandomUniqueID();
        List<UserDataUpdate> UsersUpdateSelect(Int64 FK_Users);



        public List<RoleModel> GetRoles(int idRole);
        public HttpResponses UpdateRoles(RoleModel model);

        public HttpResponses DeleteRoles(int idRole);



        #endregion Users


        HttpResponses DropDownData();
        HttpResponses CommonDropDownData();
        string GetTenantData();

        public List<ClientMenuModel> GetMenuClient(ClientMenuModel inputmodel);
        HttpResponses UpdateMenuClient(ClientMenuModel client);

        public List<MenuRoleModel> GetMenuRoleModel(MenuRoleModel menumodel);

        

        public HttpResponses RoleModelUpdate(RoleModel userRole);

        public HttpResponses userRoleDelete(RoleModel role);
        HttpResponses PostMenuModel(MenuModel menu);
        public HttpResponses UpdateMenuRole(MenuRoleModel menu);
        
        public List<RoleModel> getUserRole(RoleModel role);

        public List<MenuModel> GetMenuModel(MenuModel model);
        HttpResponses MenuDelete(MenuModel model);
       
    }
}