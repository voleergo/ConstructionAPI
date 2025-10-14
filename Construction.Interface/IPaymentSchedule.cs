using Construction.DomainModel;
using Construction.DomainModel.PaymentSchedule;
using Construction.DomainModel.Project;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Construction.Interface
{
    public interface IPaymentScheduleService
    {
        // Get payment schedules filtered by project and/or paymentSchedule
        List<PaymentScheduleModel> GetPaymentSchedules(int projectId, int paymentScheduleId);

        // Update or create a payment schedule
        HttpResponses UpdatePaymentSchedule(PaymentScheduleUpdateModel model);

        HttpResponses DeletePaymentSchedule(int id_paymentSchedule);

        List<PaymentScheduleModel> GetUpcomingPaymentReminders(int fkUser);

        HttpResponses UpdatePayment(PaymentModel model);
        List<PaymentModel> GetPayments(int projectId, int paymentScheduleId);

    }


}
