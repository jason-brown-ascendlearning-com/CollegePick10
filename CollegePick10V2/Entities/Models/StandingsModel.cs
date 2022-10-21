using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Models
{
    public class StandingsModel
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public int CurrentWeek { get; set; }
        public int SeasonTotal { get; set; }
        public string Balance { get; set; }
    }
}
