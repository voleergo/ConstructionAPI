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
}
