using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Construction.DomainModel
{
    public abstract class Tasks
    {
        public JObject input { get; set; }   
        public string IPAddres { get; set; }
        //public IOptions<GenSettings> Settings { get; set; }
        public Tasks()
        {
            input = new JObject();
            IPAddres = string.Empty;            
        }
    }
}
