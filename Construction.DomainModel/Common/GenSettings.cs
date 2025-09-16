/*----------------------------------- Model Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : GenSettings Model Class
Author     : Jinesh Kumar C
Copyright  : Voleergo Solution LLP       
Created on : 02/04/2021
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

2/04/2021	Jinesh Kumar C		

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.DomainModel
{
    public class GenSettings
    {
        public string ConnectionStrings { get; set; }
        public string Token { get; set; }
        public string DefaultConnection { get; set; }
        public string ApiUrl { get; set; }
        public string ClientUrl { get; set; }
        public bool EnableTrace { get; set; }
        public bool EnableLog { get; set; }
        public string FK_Client { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }

        public GenSettings()
        {
            ConnectionStrings = string.Empty;
            Token = string.Empty;
            DefaultConnection = string.Empty;
            ApiUrl = string.Empty;
            ClientUrl = string.Empty;
            EnableTrace = false;
            EnableLog = false;
            Currency = string.Empty;
            CurrencySymbol = string.Empty;
        }

    }
}
