using System;
using Construction.DomainModel.Common;

namespace Construction.DomainModel.User
{
    public class UserDataUpdate : BaseModel
    {
        public Int32 ID_UserProfile { get; set; }
        public Int32 FK_Users { get; set; }
        public Int32 FK_Gender { get; set; }
        public Int32 FK_Profession { get; set; }
        public Int32 FK_IdentityProofType { get; set; }
        public Int32 FK_State { get; set; }
        public Int32 FK_Country { get; set; }
        public Int32 FK_UserImages { get; set; }
        public string ImageURL { get; set; }
        public string CollegeName { get; set; }
        public Boolean IsStudent { get; set; }

        

        public string FK_Company { get; set; }
        public string ProofNumber { get; set; }
        public bool IsCompany { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string GenderName { get; set; }
        public string ProfessionName { get; set; }
        public string ProofTypeName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string CompanyName { get; set; }

        public UserDataUpdate()
        {
            ID_UserProfile = 0;
            FK_Users = 0;
            FK_Gender = 0;
            FK_Profession = 0;
            FK_IdentityProofType = 0;
            FK_State = 0;
            FK_Country = 0;
            FK_UserImages = 0;
            FK_Company = string.Empty;
            ProofNumber = string.Empty;
            ImageURL= string.Empty;
            IsCompany = false;
            Address1 = string.Empty;
            Address2 = string.Empty;
            City = string.Empty;
            PostalCode = string.Empty;
            CreatedOn = DateTime.MinValue;
            ModifiedOn = DateTime.MinValue;
            FullName = string.Empty;
            Email = string.Empty;
            MobileNumber = string.Empty;
            GenderName = string.Empty;
            ProfessionName = string.Empty;
            ProofTypeName = string.Empty;
            StateName = string.Empty;
            CountryName = string.Empty;
            CompanyName = string.Empty;
        }
    }
}
