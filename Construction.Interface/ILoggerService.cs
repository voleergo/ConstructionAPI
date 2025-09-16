/*-----------------------------------   -----------------------------------------------------------------------------------------------------------------------
Purpose    : Loging Interface Service Class
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DomainModel;

namespace Construction.Interface
{
    public interface ILoggerService
    {
        string? ConnectionStrings { get; set; }
        HttpResponses LogError(Exception ex, string functionName);
    }
}
