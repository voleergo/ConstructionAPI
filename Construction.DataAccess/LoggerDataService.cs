using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.Common;
using Construction.DomainModel.User;
using Construction.DomainModel;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace Construction.DataAccess
{
    public class LoggerDataService
    {
        private readonly string _connectionString;
        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;
        public LoggerDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HttpResponses LogError(SysErrorLogModel inputModel)
        {
            Database db = EnterpriseExtentions.GetDatabase(_connectionString);
            HttpResponses response = new HttpResponses();
            string sqlCommand = Procedures.SP_LogError;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@Procedure", DbType.String, inputModel.ErrorProcedure);
            db.AddInParameter(dbCommand, "@ErrMsg", DbType.String, inputModel.ErrorMessage);
            db.AddInParameter(dbCommand, "@ErrNum", DbType.Int32, inputModel.ErrorNumber);
            db.AddInParameter(dbCommand, "@ErrState", DbType.Int32, inputModel.ErrorState);
            db.AddInParameter(dbCommand, "@ErrorLine", DbType.Int32, inputModel.ErrorLine);
            db.AddInParameter(dbCommand, "@ErrSeverity", DbType.Int32, inputModel.ErrorSeverity);           

            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                
            }
            return response;
        }


    }
}
