using System;

namespace Construction.DomainModel
{
    public class Project
    {
        public long ID_Project { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public long? FK_Customer { get; set; }
        public decimal? EstimateAmt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ProjectType { get; set; }
        public string ProjectStatus { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
