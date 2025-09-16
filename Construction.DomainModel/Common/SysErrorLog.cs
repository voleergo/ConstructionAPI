/*----------------------------------- SysErrorLogModel Class-----------------------------------------------------------------------------------------------------------------------
Purpose    : SysErrorLogModel Class
Author     : Jinesh Kumar C
Copyright  :
Created on :04-11-2023 18:32:06
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On                                     By
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
04-11-2023 18:32:06                Jinesh Kumar C
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel
{
    public class SysErrorLogModel
    {
        public Int32 ErrorLogID { get; set; }
        public Int32 ErrorNumber { get; set; }
        public Int32 ErrorSeverity { get; set; }
        public Int32 ErrorState { get; set; }
        public string ErrorProcedure { get; set; }
        public Int32 ErrorLine { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 CreatedBy { get; set; }

        public SysErrorLogModel()
        {
            ErrorLogID = 0;
            ErrorNumber = 0;
            ErrorSeverity = 0;
            ErrorState = 0;
            ErrorProcedure = string.Empty;
            ErrorLine = 0;
            ErrorMessage = string.Empty;
            CreatedOn = DateTime.Now;
            CreatedBy = 0;

        }
    }
}
