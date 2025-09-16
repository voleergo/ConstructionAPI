using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.HttpResponse
{
    public class VerifyPhoneRequest
    {
        public string OTP { get; set; }
        public string phone { get; set; }
        public bool IsRegistration { get; set; }

        public VerifyPhoneRequest()
        {
            phone = string.Empty;
            IsRegistration = false;
            OTP = string.Empty;
        }
    }
}
