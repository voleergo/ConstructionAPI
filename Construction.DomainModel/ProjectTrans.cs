using System;

namespace Construction.DomainModel
{
    public class ProjectTrans
    {
        public long ID_ProjectTrans { get; set; }
        public long FK_Project { get; set; }
        public long FK_Level { get; set; }
        public long FK_Item { get; set; }
        public string AccountType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Qty { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
