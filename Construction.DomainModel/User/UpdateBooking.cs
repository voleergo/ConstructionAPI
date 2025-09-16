using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class UpdateBooking
    {
        public int ID_RegistrationDetails { get; set; } = 0;
        public int Status { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public string date_Reg { get; set; }

        public string time { get; set; }
    }
}
