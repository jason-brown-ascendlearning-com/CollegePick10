using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Models
{
    public class PaymentStatusModel
    {
        public bool IsLoggedIn { get; set; }
        public bool HasPaid { get; set; }
        public string PaymentDeadline { get; set; }
    }
}
