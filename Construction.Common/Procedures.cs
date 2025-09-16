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
        /* Stored Procedure - Construction */
        public const string SP_UsersSelect = "usp_SelectUsers";
        public const string SP_UsersDelete = "usp_DeleteUsers";
        public const string SP_UpdateUser = "usp_InsertUsers";
        public const string SP_SelectMenuTenant = "usp_SelectMenuTenant";
        public const string SP_InsertMenuClient = "usp_InsertMenuClient";
        public const string SP_SelectMenuRole = "usp_SelectMenuRole";
        public const string SP_db_Role = "usp_SelectRole";
        public const string SP_db_DeleteRole = "usp_DeleteRole";
        public const string SP_db_GetUserRole = "usp_GetUserRole";
        public const string Sp_UpdateMenuRole = "usp_UpdateMenuRole";
        public const string SP_UpdateMenu = "usp_UpdateNewMenu";
        public const string SP_SelectMenuData = "usp_SelectMenuData";
        public const string Sp_DeleteMenuData = "usp_DeleteMenuData";
        public const string SP_GetTenantData = "usp_GetTenantData";
        public const string SP_GetRoles = "usp_SelectRole";
        /* Stored Procedure - Construction */





        public const string SP_Users_Update = "usp_UpdateUserSignUp";
       
        public const string SP_ForgotPasswordUpdate = "usp_UpdateForgotPassword";
        public const string SP_UserPasswordUpdate = "usp_UpdateUserPassword";
        public const string SP_UpdateUserProfile = "usp_UpdateUserProfile";
        public const string SP_SelectUserProfile = "usp_SelectUserProfile";
        public const string SP_SelectEventNameAndCount = "usp_SelectEventNameAndCount";
        public const string SP_GetEventRegisterDetails = "usp_GetEventRegisterDetails";
        public const string SP_UpdateNominationDetails = "usp_UpdateNominationDetails";
        public const string SP_UpdateNominationDocuments = "usp_UpdateNominationDocuments";
        public const string SP_DeleteProjectDocumentUrl = "usp_DeleteProjectDocumentUrl";
        
        public const string SP_SelectPages = "usp_SelectPages";
        public const string SP_DeleteDoctorDetails = "usp_DeleteDoctorDetails";
        public const string SP_DeleteDepartmentDetails = "usp_DeleteDepartmentDetails";
        public const string SP_DeleteCareerDetails = "usp_DeleteCareerDetails";
        public const string SP_DeleteFacilityDetails = "usp_DeleteFacilityDetails";
        public const string SP_DeleteGalleryDetails = "usp_DeleteGalleryDetails";
        public const string SP_DeletePackageDetails = "usp_DeletePackageDetails";
        public const string SP_DeleteNewsEvents = "usp_DeleteNewsEvents";


        public const string SP_GetDoctorDetails = "usp_GetDoctorDetails";
        public const string SP_GetFacilityDetails = "usp_GetFacilityDetails";
        public const string SP_GetPackageDetails = "usp_GetPackageDetails";

        public const string SP_GetDepartmentDetails = "usp_GetDepartmentDetails";
        public const string SP_GetJobApplicationDetails = "usp_GetJobApplicationDetails";


        public const string SP_PartyMemberUpdate = "usp_UpdatePartyMember";

        public const string SP_SelectPartyMember = "usp_SelectPartyMember";


        public const string SP_GetPartyMember = "usp_GetPartyMember";


        public const string SP_Company_Update = "usp_UpdateCompany";
        public const string SP_CompanySelect = "usp_SelectCompany";
        public const string SP_CompanyDelete = "usp_DeleteCompany";

        public const string SP_EventTypes_Update = "usp_UpdateEventType";
        public const string SP_EventTypesSelect = "usp_SelectEventType";
        public const string SP_EventTypesDelete = "usp_DeleteEventTypes";

        public const string SP_Events_Update = "usp_UpdateEvents";
        public const string SP_EventsSelect = "usp_SelectEvents";
        public const string SP_EventsDelete = "usp_DeleteEvents";
        public const string SP_GetEvents = "usp_GetEvents";
        public const string SP_AddDepartmentDetails = "usp_AddDepartmentDetails";
        public const string SP_AddPackageDetails = "usp_AddPackageDetails";

        public const string SP_AddFacilityDetails = "usp_AddFacilityDetails";
        public const string SP_AddDoctorDetails = "usp_AddDoctorDetails";
        public const string SP_UserImagesUpdate = "usp_UpdateUserImages";
        public const string SP_UserFilesUpdate = "usp_UpdateUserFiles";

        public const string SP_DropDownData = "usp_GetDropDownList";
        public const string SP_SelectDropDownData = "usp_SelectDropDownData";
        public const string SP_GetCareerDetails = "usp_GetCareerDetails";
        public const string SP_GetGalleryDetails = "usp_GetGalleryDetails";
        public const string SP_AddGalleryDetails = "usp_AddGalleryDetails";


        public const string SP_AddCareerDetails = "usp_AddCareerDetails";

        public const string SP_LogError = "usp_LogError";

        public const string SP_UpdateEventRegister = "usp_UpdateEventRegister";
        public const string SP_GetEventRegister = "usp_GetEventRegister";
        public const string SP_DeleteEventRegister = "usp_DeleteEventRegister";
        public const string SP_EventRegisterDeleteEvent = "usp_DeleteEventRegisterEvent";
        public const string SP_EventRegisterCompanyWise = "usp_GetEventRegisterCompanyWise";
        public const string SP_ValidateEvent = "usp_ValidateEvent";
        

        public const string SP_ParticipantsDetails = "usp_GetParticipantsDetails";
        public const string SP_ParticipantsApprovalUpdate = "usp_UpdateParticipantsApproval";

        public const string SP_Announcement_Update = "usp_UpdateAnnouncement";
        public const string SP_AnnouncementSelect = "usp_SelectAnnouncement";
        public const string SP_AnnouncementDelete = "usp_DeleteAnnouncement";


        public const string SP_RptEventParticipationCompanyWise = "usp_RptEventParticipationCompanyWise";
        public const string SP_RptEventParticipationDetails = "usp_RptEventParticipationDetails";
        public const string SP_RptEventUnRegisteredUserDetails = "usp_RptEventUnRegisteredUserDetails";
        public const string SP_RptParticipantsDetails = "usp_RptParticipantsDetails";
        public const string SP_RptUserDetails = "usp_RptUserDetails";

        public const string SP_UpdateEventResult = "usp_UpdateEventResult";
        public const string SP_SelectEventResult = "usp_SelectEventResult";
        public const string SP_SelectEventResultApproved = "usp_SelectEventResultApproved";
        public const string SP_SelectEventResultGoroupByCompany = "usp_SelectEventResultGoroupByCompany";

        public const string SP_GetResultParticipantsDetails = "usp_GetResultParticipantsDetails";
        public const string SP_SelectEventResultTopCompany = "usp_SelectEventResultTopCompany";

        //public const string SP_DropDownData = "usp_SelectDropDownData";
        public const string SP_Get_BaseReportData = "usp_RptGetBaseReport";
        public const string SP_partialMemberexceldata = "usp_GetPartialPartyMember";

        public const string SP_DoctorSelect = "usp_DoctorSelect";
        public const string SP_GetAdminUserDetails = "usp_GetAdminUserDetails";
        //patient register procedure
        public const string SP_RegisterPatient = "usp_RegisterPatient";
        //usp_GetPatientRegistrationDetails
        public const string SP_GetPatientRegistrationDetails = "usp_GetPatientRegistrationDetails";
        //usp_UpdateUserOtp
        public const string SP_UpdateUserOtp = "usp_UpdateUserOtp";
        public const string SP_VerifyUserOtp = "usp_VerifyUserOtp";

        //[usp_BookingStatus]
        public const string SP_BookingStatus = "usp_BookingStatus";
        


    }
}
