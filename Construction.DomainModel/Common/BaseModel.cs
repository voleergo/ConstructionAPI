using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.Common
{
    public class BaseModel
    {
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public string? IPAddress { get; set; }
        public string? MACAddress { get; set; }
        public string? SearchField { get; set; }
        public string? SearchValue { get; set; }
        public BaseModel()
        {
            CreatedOn = DateTime.Now;
            CreatedBy = 0;
            ModifiedOn = DateTime.Now;
            ModifiedBy = 0;
            IsDeleted = false;
            IPAddress = string.Empty;
            SearchField = string.Empty;
            SearchValue = string.Empty;
            MACAddress = string.Empty;
        }
    }

  
}
