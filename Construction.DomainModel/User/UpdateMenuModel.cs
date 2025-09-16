using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class UpdateMenuModel
    {
        public int FK_Tenant { get; set; }
		public int FK_Menu { get; set; }
		public string MenuName { get; set; }
		public int IsActive { get; set; }
		public int MenuParent { get; set; }
		public int SortOrder { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedDate { get; set; }

    }
}
