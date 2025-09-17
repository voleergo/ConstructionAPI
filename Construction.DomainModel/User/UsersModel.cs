/*----------------------------------- UsersModel Class-----------------------------------------------------------------------------------------------------------------------
Purpose    : UsersModel Class
Author     : Jinesh Kumar C
Copyright  :
Created on :31-10-2023 22:02:34
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On                                     By
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
31-10-2023 22:02:34                Jinesh Kumar C
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DomainModel.Common;

namespace Construction.DomainModel.User
{
    public class UsersModel : BaseModel
    {
        public Int64 ID_Users { get; set; }
        public Int32 FK_Users { get; set; }
        
        public string? IsUserProfileExists { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Int32 FK_Company { get; set; }

        public string? RegistrationID { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public string? Password { get; set; }

        public string? CollegeName { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public int FK_Role { get; set; }
        public string? CompanyEmail { get; set; }
        public Int32 FK_UserRole { get; set; }
        public bool IsActive { get; set; }
        public int Company { get; set; }
        public string? UserStatus { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? IDProof { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? LastPasswordChangeDate { get; set; }
        public string? Photo { get; set; }
        public Int64 FK_UserImages { get; set; }
        public string? TmpCompany { get; set; }

        public string? MessageText { get; set; }

        public string? FK_UserRoleStr { get; set; }

        public int IsUserExist { get; set; }

        public int ID_UserProfile { get; set; }

        public bool IsMobile { get; set; }
        public string? MenuJson { get; set; }


        public bool IsSuccess { get; set; }
        public string Message { get; set; }


        public string? UserPasswordStr { get; set; }
        
        public string? CompanyIDNumber { get; set; }
        
        public string? StateName { get; set; }
        public string? ImageURL { get; set; }
        
        public int Designation { get; set; }
        
        public int FK_Gender { get; set; }
        public int FK_Profession { get; set; }
        public int FK_IdentityProofType { get; set; }
        public int FK_State { get; set; }
        public int FK_Country { get; set; }
        public string ProofNumber { get; set; }
        public bool IsStudent { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public int ID_PartyMember { get; set; }

        
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        
        

        public UsersModel()
        {
            ID_Users = 0;
            ID_PartyMember = 0;
            FK_Role = 0;
            RegistrationID = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            IsActive = false;
            FirstName = string.Empty;
            LastName = string.Empty;
            MobileNumber = string.Empty;
            Email = string.Empty;
            CompanyEmail = string.Empty;
            FK_UserRole = 0;
            IDProof = string.Empty;
            Photo = string.Empty;
            FK_UserImages = 0;
            TmpCompany = string.Empty;
            UserPasswordStr = string.Empty;
            MessageText = string.Empty;
            CompanyIDNumber = string.Empty;
            FK_UserRoleStr = string.Empty;
            ImageURL = string.Empty;
            StateName = string.Empty;
            IsUserExist = 0;
            ID_UserProfile = 0;
            FK_Gender = 0;
            FK_Profession = 0;
            FK_IdentityProofType = 0;
            FK_State = 0;
            FK_Country = 0;
            ProofNumber = string.Empty;
            //IsCompany = false;
            Address1 = string.Empty;
            Address2 = string.Empty;
            City = string.Empty;
            PostalCode = string.Empty;
            Company = 0;
            IsMobile = false;
            Designation = 0;



        }
    }
}
