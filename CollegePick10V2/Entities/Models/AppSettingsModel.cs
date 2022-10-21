using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Models
{
    public class AppSettingsModel
    {
        public Double PaymentAmount { get; set; }
        public Double PayPalFee { get; set; }
        public int TotalWeeks { get; set; }
        public string PaymentCutoff { get; set; }
        public string SeasonStart { get; set; }
        public string SeasonEnd { get; set; }
    }
}
