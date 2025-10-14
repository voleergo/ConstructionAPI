using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Construction.Common;
using Construction.DomainModel;
using Construction.DomainModel.PaymentSchedule;
using Construction.DomainModel.Project; 
using Construction.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;

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
                            model.ScheduleAmount = dataReader["ScheduleAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["ScheduleAmount"]);
                            model.ScheduleDate = dataReader["ScheduleDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ScheduleDate"]);
                            model.ID_PaymentSchedule = dataReader["ID_PaymentSchedule"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID_PaymentSchedule"]);
                            model.FK_Project = dataReader["FK_Project"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["FK_Project"]);
                            model.FK_User = dataReader["FK_User"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["FK_User"]);
                            model.AmountReceived = dataReader["AmountReceived"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dataReader["AmountReceived"]);
                            model.AmountDue = dataReader["AmountDue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dataReader["AmountDue"]);

                            model.ReceivedDate = dataReader["ReceivedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ReceivedDate"]);
                            model.ProjectName = dataReader["ProjectName"] == DBNull.Value ? null : dataReader["ProjectName"].ToString();


                            model.CreatedBy = dataReader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CreatedBy"]);
                            model.ModifiedBy = dataReader["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ModifiedBy"]);
                            model.CreatedOn = dataReader["CreatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["CreatedOn"]);
                            model.ModifiedOn = dataReader["ModifiedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ModifiedOn"]);

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
            db.AddInParameter(dbCommand, "@ScheduleDate", DbType.Date, model.ScheduleDate);  // DateTime OK
            db.AddInParameter(dbCommand, "@AmountReceived", DbType.Decimal, model.AmountReceived ?? 0);
            db.AddInParameter(dbCommand, "@ReceivedDate", DbType.Date, model.ReceivedDate);  // DateTime OK
            db.AddInParameter(dbCommand, "@ModifiedBy", DbType.Int32, model.ModifiedBy);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, model.IsActive);
            db.AddInParameter(dbCommand, "@IsDeleted", DbType.Boolean, model.IsDeleted);
            db.AddInParameter(dbCommand, "@PaymentStatus", DbType.String, model.PaymentStatus);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        response.ResponseCode = dataReader["ResponseCode"] != DBNull.Value
                            ? Convert.ToString(dataReader["ResponseCode"])
                            : "0";
                        response.ResponseMessage = dataReader["ResponseMessage"] != DBNull.Value
                            ? Convert.ToString(dataReader["ResponseMessage"])
                            : string.Empty;
                        response.ResponseStatus = dataReader["ResponseStatus"] != DBNull.Value
                            && Convert.ToBoolean(dataReader["ResponseStatus"]);
                        response.ResponseID = dataReader["ResponseID"] != DBNull.Value
                            ? Convert.ToInt64(dataReader["ResponseID"])
                            : 0;
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
            string sqlCommand = Procedures.SP_DeletePayment;                
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_PaymentSchedule", DbType.Int32, id_paymentSchedule);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        response.ResponseCode = Convert.ToString(dataReader["ResponseCode"]);
                        response.ResponseMessage = Convert.ToString(dataReader["ResponseMessage"]);
                        response.ResponseStatus = Convert.ToBoolean(dataReader["ResponseStatus"]);
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


        public List<PaymentScheduleModel> GetUpcomingPaymentReminders(int fkUser)
        {
            List<PaymentScheduleModel> resultList = new List<PaymentScheduleModel>();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = "usp_GetUpcomingPaymentReminders";  // name of the SP
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            db.AddInParameter(dbCommand, "@FK_User", DbType.Int32, fkUser);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        PaymentScheduleModel model = new PaymentScheduleModel
                        {
                            ID_PaymentSchedule = dataReader["ID_PaymentSchedule"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID_PaymentSchedule"]),
                            FK_Project = dataReader["FK_Project"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["FK_Project"]),
                            ProjectName = dataReader["ProjectName"] == DBNull.Value ? null : dataReader["ProjectName"].ToString(),
                            ScheduleAmount = dataReader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["Amount"]),
                            ScheduleDate = dataReader["ScheduleDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ScheduleDate"]),
                            ReceivedDate = dataReader["RecievedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["RecievedDate"]),
                            FK_User = fkUser,
                            IsActive = true,
                            IsDeleted = false
                        };

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
        public HttpResponses UpdatePayment(PaymentModel model)
        {
            HttpResponses response = new HttpResponses();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);

            string sqlCommand = Procedures.SP_UpdatePayment;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            // Match parameters with stored procedure definition
            db.AddInParameter(dbCommand, "@ID_PaymentSchedule", DbType.Int32, model.ID_PaymentSchedule);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int32, model.FK_Project);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int32, model.FK_User);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, model.Amount);
            db.AddInParameter(dbCommand, "@Mode", DbType.String, model.Mode); // Use 'Mode' for payment method
            db.AddInParameter(dbCommand, "@ScheduleDate", DbType.Date, model.ScheduleDate);
            db.AddInParameter(dbCommand, "@RecievedDate", DbType.Date, model.RecievedDate);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, model.IsActive);
            db.AddInParameter(dbCommand, "@IsDelete", DbType.Boolean, model.IsDelete);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        response.ResponseCode = dataReader["ResponseCode"]?.ToString() ?? "0";
                        response.ResponseMessage = dataReader["ResponseMessage"]?.ToString() ?? string.Empty;
                        response.ResponseStatus = dataReader["ResponseStatus"] != DBNull.Value && Convert.ToBoolean(dataReader["ResponseStatus"]);
                        response.ResponseID = dataReader["ResponseID"] == DBNull.Value ? 0 : Convert.ToInt64(dataReader["ResponseID"]);
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

        public List<PaymentModel> GetPayments(int projectId, int paymentScheduleId)
        {
            List<PaymentModel> resultList = new List<PaymentModel>();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            string sqlCommand = Procedures.SP_GetPayment;  // your procedure name

            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;

            db.AddInParameter(dbCommand, "@ID_PaymentSchedule", DbType.Int32, paymentScheduleId);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int32, projectId);

            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    // First result set: payment records
                    while (reader.Read())
                    {
                        PaymentModel model = new PaymentModel
                        {
                            ID_PaymentSchedule = reader["ID_PaymentSchedule"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PaymentSchedule"]),
                            FK_Project = reader["FK_Project"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FK_Project"]),
                            ProjectName = reader["ProjectName"] == DBNull.Value ? string.Empty : reader["ProjectName"].ToString(),
                            FK_User = reader["FK_User"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FK_User"]),
                            Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                            Mode = reader["Mode"] == DBNull.Value ? string.Empty : reader["Mode"].ToString(),
                            ScheduleDate = reader["ScheduleDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ScheduleDate"]),
                            RecievedDate = reader["RecievedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["RecievedDate"]),
                            CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"]),
                            ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ModifiedBy"]),
                            CreatedOn = reader["CreatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedOn"]),
                            ModifiedOn = reader["ModifiedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedOn"]),
                            IsActive = reader["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsActive"]),
                            IsDelete = reader["IsDelete"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsDelete"]),
                            ID_Users = reader["ID_Users"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_Users"]),
                            Budget = reader["Budget"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["Budget"])
                        };
                        resultList.Add(model);
                    }

                    // Move to next result set: totals (Total_Due, Received_Amount, Balance_Amount)
                    if (reader.NextResult() && reader.Read())
                    {
                        decimal totalDue = reader["Total_Due"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Total_Due"]);
                        decimal receivedAmount = reader["Received_Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Received_Amount"]);
                        decimal balanceAmount = reader["Balance_Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Balance_Amount"]);

                        foreach (var model in resultList)
                        {
                            model.TotalDue = totalDue;
                            model.ReceivedAmount = receivedAmount;
                            model.BalanceAmount = balanceAmount;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return resultList;
        }




    }

}
