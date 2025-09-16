
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Text;


namespace Construction.DomainModel.Common
{
    public static class EmailTemplateEditor
    {
        public static string EditTemplate(string templatePath, string? password, string ticketBody, string regID = "")
        {
            StreamReader str = new StreamReader(templatePath);
            string MailText = str.ReadToEnd();
            str.Close();
            string regIDString = " Your Registration ID is: " + regID;
            //MailText = MailText.Replace("~1~", emailModel.Subject).Replace("~2~", ticketBody).Replace("~3~", emailModel.EmailID).Replace("~4~", password).Replace("~5~", regIDString).Replace("~6~", emailModel.ClientUrl);
            return MailText;
        }
    }
}
