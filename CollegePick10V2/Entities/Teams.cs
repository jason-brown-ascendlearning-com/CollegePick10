using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities
{
    public class Teams
    {
        [Key]
        public int TeamID { get; set; }
        public string FulleName { get; set; }
        public string EspnName { get; set; }
        public string Abbreviation { get; set; }
        public int? Rank { get; set; }

    }
}
