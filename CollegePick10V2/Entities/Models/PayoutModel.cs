using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Models
{
    public class PayoutModel
    {
        public string SeasonTotal { get; set; }
        public string WeeklyTotal { get; set; }
        public List<PaymentModel> Payouts { get; set; }
    }
}
