using System;

namespace Construction.DomainModel
{
    public class UserRole
    {
        public long ID_UserRole { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
