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
        public int ID_ServiceCategory { get; set; }
        public  string CategoryName { get; set; }
        public int FK_ProjectType { get; set; }
    }
}
