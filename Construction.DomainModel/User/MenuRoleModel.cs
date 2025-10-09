using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class MenuRoleModel
    {
        public bool IsAssigned;

        public int? FK_MenuTenant { get; set; }
        public int? FK_MenuClient { get; set; }
        public int? FK_Role { get; set; }
        public int? FK_Tenant { get; set; }

        public int? FK_Menu { get; set; }
        public string? MenuName { get; set; } = string.Empty;
        public int? MenuParent { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsAdd { get; set; }
        public bool? IsEdit { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsView { get; set; }
        public bool? IsPrint { get; set; }
        public bool? Active { get; set; }
        public string? MenuRole { get; set; }
        public string? UserMenu { get; set; }
        public int? IsSoftwareVendor{ get; set; }
        public int? FK_Icons { get; set; }
        public string? IconDisplayName { get; set; } = string.Empty;
        public string? IconName { get; set; } = string.Empty;
        public string? JsonData { get; set; } = string.Empty;
        public string? MenuJson { get; set; }= string.Empty;
        public string? ModuleCode { get; set; }
        public int Header { get; set; }
        public int ID_Menufilter { get; set; }
    }
}
