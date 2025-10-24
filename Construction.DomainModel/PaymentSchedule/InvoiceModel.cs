using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.PaymentSchedule
{
    public class InvoiceModel
    {
        public int ID_Invoice { get; set; }
        public string Code { get; set; }
        public string Mode { get; set; }
        public decimal Amount { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? FK_Payment { get; set; }
        public int FK_Customer { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public int FK_Project { get; set; }
        public string ProjectName { get; set; }
        public decimal? Budget { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? RecievedDate { get; set; }
        public string Description { get; set; }
        public decimal? Discount { get; set; }
        public decimal? RecievedAmount { get; set; }
        public decimal? ScheduledAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public decimal? CalculatedAmount { get; set; }
    }
}
