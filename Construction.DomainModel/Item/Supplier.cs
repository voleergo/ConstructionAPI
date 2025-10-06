using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.Item
{
    public class SupplierModel
    {
        public int ID_Supplier { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int FK_ServiceCategory { get; set; }
        public string CategoryName { get; set; }
    }

    public class AddSupplierModel
    {
        public int ID_Supplier { get; set; }
        public string SupplierName { get; set; }
        public int CreatedBy { get; set; }
        public int FK_ServiceCategory { get; set; }
    }
    
        public class AddCategoryAndSupplierModel
        {
            public int ID_ServiceCategory { get; set; }   // 0 if new category
            public string? CategoryName { get; set; }     // required if new
            public int? FK_ProjectType { get; set; }      // required if new
            public string SupplierName { get; set; }
            public int CreatedBy { get; set; }
        }
    

}
