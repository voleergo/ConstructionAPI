using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class GeneralConfigurations
    {
        public int ID_GeneralConfiguration { get;set; }
        public string? Value { get;set; }
        public string? Description { get;set; }

        public GeneralConfigurations() {

            ID_GeneralConfiguration = 0;
            Value = string.Empty;
            Description = string.Empty;
        }

    }
}
