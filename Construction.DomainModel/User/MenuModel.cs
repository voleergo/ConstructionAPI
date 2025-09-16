using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class MenuModel
    {
        public int CreatedBy { get; set; }
        public int FK_Menu { get; set; }
        public string? MenuName { get; set; }
        public string? MenuUrl { get; set; }
        public string? ModuleCode { get; set; }
        public int VenderMenu { get; set; }
        public string? JsonData { get; set; }
        public Boolean IsActive { get; set; }
        public int FK_Tenant { get; set; }
        public int FK_Pages { get; set; }
        public Boolean IsDelete { get; set; }
        public int ID_Menu { get; set; } // Changed from FK_Menu
        
        public int? MenuParent { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public MenuModel()
        {
            CreatedBy = 0;
            FK_Menu = 0;
            MenuName = string.Empty;
            MenuUrl = string.Empty;
            ModuleCode = string.Empty;
            VenderMenu = 0;
            JsonData = string.Empty;
            IsActive =true;

        }

    }
}
