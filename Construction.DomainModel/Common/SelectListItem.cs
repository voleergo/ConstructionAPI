using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel
{
    public class SelectListItem
    {
        public string? Text { get; set; }
        public string? Value { get; set; }

        public SelectListItem() {
            Text = string.Empty;
            Value = string.Empty;
        }
    }
}
