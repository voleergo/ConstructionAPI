using Construction.DataAccess;
using Construction.DomainModel;
using Construction.DomainModel.Item;
using Construction.DomainModel.PaymentSchedule;
using Construction.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;

namespace Construction.Service
{
    public class PaymentScheduleService : IPaymentScheduleService
    {
        private readonly string _connectionString;

        // Pass the connection string via constructor
        public PaymentScheduleService(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");

            _connectionString = connectionString;
        }

        // Method to get payment schedules using the model as input
        public List<PaymentScheduleModel> GetPaymentSchedules(int projectId, int paymentScheduleId)
        {
            // Create data service instance using the connection string
            PaymentScheduleDataService dataService = new PaymentScheduleDataService(_connectionString);


            // Call data service to get the list
            return dataService.GetPaymentSchedules(projectId, paymentScheduleId);
        }

        public HttpResponses UpdatePaymentSchedule(PaymentScheduleUpdateModel model)
        {
            PaymentScheduleDataService dataService = new PaymentScheduleDataService(_connectionString);
            return dataService.UpdatePaymentSchedule(model);
        }

        public HttpResponses DeletePaymentSchedule(int id_paymentSchedule)
        {
            PaymentScheduleDataService dataService = new PaymentScheduleDataService(_connectionString);
            return dataService.DeletePaymentSchedule(id_paymentSchedule);
        }

        public List<PaymentScheduleModel> GetUpcomingPaymentReminders(int fkUser)
        {
            PaymentScheduleDataService dataService = new PaymentScheduleDataService(_connectionString);
            return dataService.GetUpcomingPaymentReminders(fkUser);
        }
        public HttpResponses UpdatePayment(PaymentModel model)
        {
            PaymentScheduleDataService dataService = new PaymentScheduleDataService(_connectionString);
            return dataService.UpdatePayment(model);
        }
        public List<PaymentModel> GetPayments(int projectId, int paymentScheduleId)
        {
            PaymentScheduleDataService dataService = new PaymentScheduleDataService(_connectionString);
            return dataService.GetPayments(projectId, paymentScheduleId);
        }

    }
}
