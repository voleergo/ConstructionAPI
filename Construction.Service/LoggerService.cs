/*-----------------------------------   -----------------------------------------------------------------------------------------------------------------------
Purpose    : Loging Service Class
Author     : Jinesh Kumar C
Copyright  :
Created on :03-11-2023 10:29:07
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On                          By                    TaskID          Description
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
03-11-2023 10:29:07          Jinesh Kumar C                         initially  created
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DataAccess;
using Construction.DomainModel;
using Construction.Interface;

namespace Construction.Service
{
    public class LoggerService: ILoggerService
    {
        public string ConnectionStrings { get; set; }
        public HttpResponses LogError(Exception ex,string functionName)
        {
            SysErrorLogModel sysErrorLog = new SysErrorLogModel();
            sysErrorLog.ErrorProcedure = functionName;
            sysErrorLog.ErrorMessage = ex.Message;
            sysErrorLog.ErrorNumber = 0;
            sysErrorLog.ErrorState = 0;
            sysErrorLog.ErrorLine = 0;
            sysErrorLog.ErrorSeverity = 0;

            LoggerDataService loggerDataService = new LoggerDataService(ConnectionStrings);
            return loggerDataService.LogError(sysErrorLog);
        }
    }
}
