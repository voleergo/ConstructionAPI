//projectModel


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.Project
{
    public class ProjectModel
    {
        public int projectID { get; set; }
        public string projectName { get; set; }

        public string projectType { get; set; }
        public string projectStatus { get; set; }
        public string? customerCode { get; set; }
        public int? FK_Customer { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public decimal? budget { get; set; }
        public decimal? expenses { get; set; }
        public string description { get; set; }
        public int? FK_User { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string projectCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; } = 0;
        public DateTime? ModifiedOn { get; set; }
        public string CustomerName { get; set; } 
        public string CustomerAddress { get; set; } 
        public string MobileNumber { get; set; } 
        public string Email { get; set; } 

        public string json { get; set; } = string.Empty;

        public int? ModifiedBy { get; set; } = 0;
   
    }
}