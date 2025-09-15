using System;

namespace Construction.DomainModel
{
    public class Supplier
    {
        public long ID_Supplier { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
