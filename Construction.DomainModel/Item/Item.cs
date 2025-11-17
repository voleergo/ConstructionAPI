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
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? ServiceCategoryName { get; set; }
        public string? SupplierName { get; set; }

        public string Unit { get; set; }
        public decimal TotalPrice { get; set; }
        public int FK_Supplier { get; set; }
        public string? ProjectName { get; set; }

        public int UserID { get; set; }
    }
    public class Item
    {
        public int ID_ServiceCategory { get; set; }
        public  string CategoryName { get; set; }
        public int FK_ProjectType { get; set; }
    }
}
