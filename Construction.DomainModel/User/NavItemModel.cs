using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class NavItemModel
    {
        public string Title { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
      
    }
    public class NavMenu :  NavItemModel
    {
        public List<NavItemModel> Children { get; set; }= new List<NavItemModel>();

    }
    
}
