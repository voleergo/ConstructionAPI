using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class MenuPermission
    {
        public int FK_Menu { get; set; }
        public int FK_Icons { get; set; }
        public string MenuName { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsView { get; set; }
        public bool IsPrint { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public int Header { get; set; }
        public string Url { get; set; }
        public string IconName { get; set; }
        public string? ModuleCode { get; set; }
    }
}
