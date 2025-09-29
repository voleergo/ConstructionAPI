using Construction.Common;
using Construction.DomainModel;
using Construction.DomainModel.Project; // Use the actual namespace
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Construction.DomainModel.PaymentSchedule;

namespace Construction.DataAccess
{


    public class PaymentScheduleDataService
    {
        public readonly string _connectionString;
        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        public PaymentScheduleDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<PaymentScheduleModel> GetPaymentSchedules(int projectId, int paymentScheduleId)
        {
            List<PaymentScheduleModel> resultList = new List<PaymentScheduleModel>();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_GetPaymentSchedule;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            db.AddInParameter(dbCommand, "@ID_PaymentSchedule", DbType.Int64, paymentScheduleId);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int64, projectId); // pass project

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        PaymentScheduleModel model = new PaymentScheduleModel();
                        {
                            model.ID_PaymentSchedule = dataReader["ID_PaymentSchedule"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID_PaymentSchedule"]);
                            model.FK_Project = dataReader["FK_Project"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["FK_Project"]);
                            model.FK_User = dataReader["FK_User"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["FK_User"]);

                            model.ScheduleAmount = dataReader["ScheduleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["ScheduleAmount"]);
                            model.ScheduleDate = dataReader["ScheduleDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ScheduleDate"]);

                            model.AmountReceived = dataReader["AmountReceived"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dataReader["AmountReceived"]);
                            model.AmountDue = dataReader["AmountDue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dataReader["AmountDue"]);
                            model.ReceivedDate = (DateTime)(dataReader["ReceivedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ReceivedDate"]));

                            model.CreatedBy = dataReader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CreatedBy"]);
                            model.ModifiedBy = dataReader["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ModifiedBy"]);
                            model.CreatedOn = dataReader["CreatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["CreatedOn"]);
                            model.ModifiedOn = (DateTime)(dataReader["ModifiedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ModifiedOn"]));

                            model.IsActive = dataReader["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsActive"]);
                            model.IsDeleted = dataReader["IsDeleted"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsDeleted"]);

                            model.PaymentStatus = dataReader["PaymentStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["PaymentStatus"]);

                            // Extra fields
                            model.UserID = dataReader["ID_Users"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID_Users"]);
                            model.Budget = dataReader["Budget"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dataReader["Budget"]);

                        }
                        ;

                        resultList.Add(model);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return resultList;
        }


        public HttpResponses UpdatePaymentSchedule(PaymentScheduleUpdateModel model)
        {
            HttpResponses response = new HttpResponses();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_UpdatePaymentSchedule;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            // Match parameters with stored procedure definition
            db.AddInParameter(dbCommand, "@ID_PaymentSchedule", DbType.Int32, model.ID_PaymentSchedule);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int32, model.FK_Project);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int32, model.FK_User);
            db.AddInParameter(dbCommand, "@ScheduleAmount", DbType.Decimal, model.ScheduleAmount);
            db.AddInParameter(dbCommand, "@ScheduleDate", DbType.Date, model.ScheduleDate);
            db.AddInParameter(dbCommand, "@AmountReceived", DbType.Decimal, model.AmountReceived);
            db.AddInParameter(dbCommand, "@ReceivedDate", DbType.Date, model.ReceivedDate);
            db.AddInParameter(dbCommand, "@CreatedOn", DbType.Int32, model.CreatedOn);
            //db.AddInParameter(dbCommand, "@ModifiedOn", DbType.Int32, model.ModifiedOn);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, model.IsActive);
            db.AddInParameter(dbCommand, "@IsDeleted", DbType.Boolean, model.IsDeleted);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        response.ResponseCode = dataReader["ResponseCode"] != DBNull.Value ? Convert.ToString(dataReader["ResponseCode"]) : "0";
                        response.ResponseMessage = dataReader["ResponseMessage"] != DBNull.Value ? Convert.ToString(dataReader["ResponseMessage"]) : string.Empty;
                        response.ResponseStatus = dataReader["ResponseStatus"] != DBNull.Value && Convert.ToBoolean(dataReader["ResponseStatus"]);
                        response.ResponseID = dataReader["ResponseID"] != DBNull.Value ? Convert.ToInt64(dataReader["ResponseID"]) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = "0";
                response.ResponseMessage = ex.Message;
                response.ResponseStatus = false;
            }

            return response;
        }

        public HttpResponses DeletePaymentSchedule(int id_paymentSchedule)
        {
            HttpResponses response = new HttpResponses();
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_DeletePaymentSchedule;                
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_paymentSchedule", DbType.Int32, id_paymentSchedule);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        response.ResponseCode = Convert.ToString(dataReader["ResponseCode"]);
                        response.ResponseMessage = Convert.ToString(dataReader["ResponseMessage"]);
                        response.ResponseStatus = Convert.ToBoolean(dataReader["ResponseStatus"]);
                        // Note: Your stored procedure doesn't return ResponseID, so remove this line
                        response.ResponseID = Convert.ToInt64(dataReader["ResponseID"]);
                    }
                }
            }
            catch (Exception e)
            {
                // Add proper error handling instead of just throwing
                response.ResponseCode = "0";
                response.ResponseMessage = e.Message;
                response.ResponseStatus = false;
            }
            return response;
        }
    }

}
