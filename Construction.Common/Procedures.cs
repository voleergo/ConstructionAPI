/*----------------------------------- Data Access Repository Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Jinesh Kumar C
Copyright  : Voleergo Technologies
Created on : 22/05/2019
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
22/05/2019	Jinesh Kumar C		
16/05/2021  Athul TP    Added Procedure SP_POS_GetPurchaseItemDetails(Purchase Grid Item)
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Construction.Common
{
    public static class Procedures
    {
        //Project Management
        public const string SP_GetProject = "usp_SelectProject";
        public const string SP_DeleteProject = "usp_Project_Delete";
        public const string SP_UpdateProject = "usp_UpdateProject";

        public const string SP_GetProjectStatus = "usp_GetProjectStatus";
        public const string SP_GetProjectType = "usp_GetProjectType";

        //ServiceCategory
        public const string SP_GetServiceCategory = "usp_GetServiceCategory";
        public const string SP_DeleteServiceCategory = "usp_DeleteServiceCategory";
        public const string SP_UpdateServiceCategory = "usp_UpdateServiceCategory";


        //ProjectService Management
        public const string SP_UpdateProjectService = "usp_UpdateProjectService";
        public const string SP_DeleteProjectService = "usp_DeleteProjectService";
        public const string SP_GetProjectService = "usp_GetProjectService";

        //User Management
        public const string SP_UsersDelete = "usp_User_Delete";
        public const string SP_UpdateUser = "usp_UserUpdate";
        public const string SP_GetUsers = "usp_User_GetAll";
        //Role Management
        public const string SP_GetRoles = "usp_SelectRole";
        public const string SP_InsertRole = "usp_InsertRole";
        public const string SP_DeleteRole = "usp_DeleteRole";

        public const string SP_SelectMenuTenant = "usp_SelectMenuTenant";
        public const string SP_InsertMenuClient = "usp_InsertMenuClient";

        //Menu Role Management
        public const string SP_DeleteMenuClient = "usp_DeleteMenuClient";
        public const string SP_SelectMenuRole = "usp_SelectMenuRole";      
        public const string Sp_UpdateMenuRole = "usp_UpdateMenuRole";

        //Menu Management
        public const string SP_UpdateMenu = "usp_UpdateNewMenu";
        public const string SP_SelectMenuData = "usp_SelectMenuData";
        public const string Sp_DeleteMenuData = "usp_DeleteMenuData";


        public const string SP_GetTenantData = "usp_GetTenantData";
        
        //login    
        public const string SP_ValidateLogin = "usp_ValidateLogin";
        public const string SP_ForgotPassword = "usp_ForgotPassword";


        public const string SP_Users_Update = "usp_UpdateUserSignUp";
       
        
        public const string SP_UserPasswordUpdate = "usp_UpdateUserPassword";
        public const string SP_UpdateUserProfile = "usp_UpdateUserProfile";
        public const string SP_SelectUserProfile = "usp_SelectUserProfile";
        

      

        public const string SP_UserImagesUpdate = "usp_UpdateUserImages";
        public const string SP_UserFilesUpdate = "usp_UpdateUserFiles";

        public const string SP_DropDownData = "usp_GetDropDownList";
        public const string SP_SelectDropDownData = "usp_SelectDropDownData";
       
        public const string SP_LogError = "usp_LogError";

        


    }
}
