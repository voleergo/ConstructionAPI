using System;

namespace Construction.DomainModel.PaymentSchedule
{
    public class PaymentScheduleModel
    {
        public int ID_PaymentSchedule { get; set; }
        public int FK_Project { get; set; }
        public int FK_User { get; set; }
        public decimal ScheduleAmount { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public decimal? AmountReceived { get; set; }
        public decimal? AmountDue { get; set; }  // changed from string → decimal
        public DateTime ReceivedDate { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string PaymentStatus { get; set; }

        // Extra fields from join
        public int UserID { get; set; }   // from Users.ID_Users
        public decimal? Budget { get; set; } // from Projects.Budget
    }


    public class PaymentScheduleUpdateModel
    {
        public int ID_PaymentSchedule { get; set; }
        public int FK_Project { get; set; }
        public int FK_User { get; set; }
        public decimal ScheduleAmount { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public decimal? AmountReceived { get; set; }
        public DateTime ReceivedDate { get; set; }
        public int CreatedOn { get; set; }
        public DateTime ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string PaymentStatus { get; set; }

    }
}
