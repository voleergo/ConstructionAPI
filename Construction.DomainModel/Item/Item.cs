using System;
using System.ComponentModel;
using Construction.DomainModel.Common;

namespace Construction.DomainModel.Item
{
    public class ProjectServiceModel
    {
        public int ID_ProjectService { get; set; }
        public int FK_ServiceCategory { get; set; }
        public string ProjectService { get; set; }
        public int FK_Project { get; set; }
        public  int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int FK_Supplier { get; set; }
    }
    public class Item
    {
        public int ID_Item { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
