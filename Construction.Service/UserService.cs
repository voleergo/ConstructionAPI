/*----------------------------------- UsersDataService  -----------------------------------------------------------------------------------------------------------------------
Purpose    : AuthService Class
Author     : Jinesh Kumar C
Copyright  :
Created on :01-11-2023 09:32:03
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On                          By                    TaskID          Description
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
01-11-2023 09:32:03          Jinesh Kumar C                       AuthService Class initially  created
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using Construction.DomainModel;
using Construction.DomainModel.User;
using Construction.Interface;
using Construction.DataAccess;
using Newtonsoft.Json;
using Construction.Common;


namespace Construction.Service
{
    public class UserService : IUserService
    {
        public string? ConnectionStrings { get; set; }

       

        public UsersModel ValidateLogin(LoginModel model)
        {
           
            // Call the DataService
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.ValidateLogin(model);

            
        }



        public HttpResponses ForgotPassword(PasswordModel inputModel)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);

            return dataService.ForgotPassword(inputModel);
        }


        public HttpResponses UsersUpdate(UserUpdateModel inputModel)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.UsersUpdate(inputModel);
        }


        public List<RoleModel> GetRoles(int idRole)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.GetRoles(idRole);
        }


        public HttpResponses UpdateRoles(JsonModel package)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.UpdateRoles(package);
        }


        public HttpResponses DeleteRoles(int idRole)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.DeleteRoles(idRole);
        }

      
     public HttpResponses UsersDelete(UsersModel inputModel)
     {
         UserDataService dataService = new UserDataService(ConnectionStrings);
         return dataService.UsersDelete(inputModel);
     }


     public List<UsersModel> GetUsers(UsersModel inputModel)
     {
         UserDataService dataService = new UserDataService(ConnectionStrings);
         return dataService.GetUsers(inputModel);
     }
      


        
        public List<UserDataUpdate> UsersUpdateSelect(Int64 FK_Users)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.UsersUpdateSelect(FK_Users);
        }


        public HttpResponses UserDataUpdate(UsersModel inputModel)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.UsersDataUpdate(inputModel);
        }




        public HttpResponses DropDownData()
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.DropDownData();
        }
        public HttpResponses CommonDropDownData()
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.CommonDropDownData();
        }

        public string GenerateRandomUniqueID()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string uniqueID = new string(Enumerable.Range(0, 10).Select(_ => chars[random.Next(chars.Length)]).ToArray());

            Console.WriteLine($"Generated UniqueID: {uniqueID}");

            return uniqueID;
        }


      







        public List<ClientMenuModel> GetMenuClient(ClientMenuModel inputmodel)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.GetMenuClient(inputmodel);
        }
        public HttpResponses UpdateMenuClient(ClientMenuModel client)
        {

            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.UpdateMenuClient(client);

        }

        public string GetTenantData()
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.GetTenantData();
        }


        public List<MenuRoleModel> GetMenuRoleModel(MenuRoleModel menumodel)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.GetMenuRoleModel(menumodel);
        }
        public HttpResponses UpdateMenuRole(MenuRoleModel menu)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);

            // MenuJson can be generated or passed; if you only want updates, make sure it matches DB fields
            menu.MenuJson = GenerateMenu(menu.JsonData);

            return dataService.UpdateMenuRole(menu);
        }
        

        private string GenerateMenu(string menuString)
        {
            List<MenuPermission> permissions = JsonConvert.DeserializeObject<List<MenuPermission>>(menuString);

            List<NavMenu> mnuList = new List<NavMenu>();
            var parentMenus = permissions.Where(menu => menu.Header == 1).ToList();

            foreach (var item in parentMenus)
            {
                List<MenuPermission> permissionList = new List<MenuPermission>();
                NavMenu navMenu = new NavMenu();
                permissionList = permissions.Where(parent => parent.ModuleCode == item.ModuleCode && parent.Header == 0).ToList();
                navMenu.Children = permissionList.Select(root => new NavItemModel
                {
                    Path = root.Url,
                    Title = root.MenuName,
                    Icon = "icon('" + root.IconName + "')"
                }).ToList();
                navMenu.Title = item.MenuName;
                navMenu.Path = item.Url;
                navMenu.Icon = "icon('" + item.IconName + "')";
                mnuList.Add(navMenu);
            }
            return JsonConvert.SerializeObject(mnuList);
        }


        //public HttpResponses RoleModelUpdate(RoleModel userRole)
        //{
        //    UserDataService dataService = new UserDataService(ConnectionStrings);
        //    return dataService.RoleModelUpdate(userRole);
        //}
        //public HttpResponses userRoleDelete(RoleModel userRole)
        //{
        //    UserDataService dataService = new UserDataService(ConnectionStrings);
        //    return dataService.userRoleDelete(userRole);
        //}

        //public List<RoleModel> getUserRole(RoleModel userRole)
        //{
        //    UserDataService dataService = new UserDataService(ConnectionStrings);
        //    return dataService.getUserRole(userRole);
        //}
        public HttpResponses PostMenuModel(MenuModel menu)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.PostMenuModel(menu);
        }
        public HttpResponses MenuDelete(MenuModel model)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.MenuDelete(model);
        }
        public List<MenuModel> GetMenuModel(MenuModel menu)
        {
            UserDataService dataService = new UserDataService(ConnectionStrings);
            return dataService.GetMenuModel(menu);
        }




        public HttpResponses RoleModelUpdate(RoleModel userRole)
        {
            throw new NotImplementedException();
        }

        public HttpResponses userRoleDelete(RoleModel role)
        {
            throw new NotImplementedException();
        }

        public List<RoleModel> getUserRole(RoleModel role)
        {
            throw new NotImplementedException();
        }

        public List<RoleModel> GetRoles(RoleModel role)
        {
            throw new NotImplementedException();
        }

        public List<UsersModel> UsersSelect(UsersModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}