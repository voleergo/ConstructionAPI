using System;

namespace Construction.DomainModel
{
    public class User
    {
        public long ID_Users { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public long? FK_UserRoles { get; set; }
        public string UserStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
