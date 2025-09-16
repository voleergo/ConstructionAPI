using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DomainModel;

namespace Construction.Interface
{
    public interface IEmailService
    {
        HttpResponses SendEmail(EmailModel model);
    }
}