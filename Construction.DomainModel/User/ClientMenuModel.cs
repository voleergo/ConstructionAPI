using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class ClientMenuModel
    {
        public int? ID_MenuClient { get; set; }
        public int? FK_Tenant { get; set; }
        public int? FK_Menu { get; set; }
        public string? UserMenu { get; set; }
        public string? MenuName { get; set; } = string.Empty;
        public Boolean? IsActive { get; set; }
        public int? MenuParent { get; set; }
        public int? SortOrder { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; } 
        public DateTime? ModifiedDate { get; set; }
       
        public string? JsonData { get; set; } = string.Empty;
        public  string? ModuleCode {  get; set; } = string.Empty;
        public string? ModuleName { get; set; } = string.Empty;
        public int? Header { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDelete { get; set; }
    }
}
