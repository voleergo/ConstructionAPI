using System;

namespace Construction.DomainModel.PaymentSchedule
{
    public class PaymentScheduleModel
    {
        public int ID_PaymentSchedule { get; set; }
        public int FK_Project { get; set; }
        public int FK_User { get; set; }
        public string ProjectName { get; set; }
        public decimal ScheduleAmount { get; set; }
        public DateTime? ScheduleDate { get; set; }   // Changed from DateOnly → DateTime
        public decimal? AmountReceived { get; set; }
        public decimal? AmountDue { get; set; }       // Computed in DB
        public DateTime? ReceivedDate { get; set; }   // Changed from DateOnly → DateTime
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string PaymentStatus { get; set; }

        // Extra fields from join
        public int UserID { get; set; }
        public decimal? Budget { get; set; }
    }

    public class PaymentScheduleUpdateModel
    {
        public int ID_PaymentSchedule { get; set; }
        public int FK_Project { get; set; }
        public int FK_User { get; set; }
        public decimal ScheduleAmount { get; set; }
        public DateTime? ScheduleDate { get; set; }    // DateTime now
        public decimal? AmountReceived { get; set; }
        public DateTime? ReceivedDate { get; set; }    // DateTime now
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public string Mode { get; set; }
    }
    public class PaymentModel
    {
        public int ID_PaymentSchedule { get; set; }
        public int FK_Project { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public int FK_User { get; set; }
        public decimal Amount { get; set; }
        public string Mode { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public DateTime? RecievedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; } = false;
        public int ID_Users { get; set; }
        public decimal? Budget { get; set; }

        // Extra fields for totals
        public decimal? TotalDue { get; set; }
        public decimal? ReceivedAmount { get; set; }
        
        public decimal? BalanceAmount { get; set; }

        public string? Description { get; set; }

       public decimal? Discount { get; set; }

       // public decimal? CalculatedAmount { get; set; }
    }
}


