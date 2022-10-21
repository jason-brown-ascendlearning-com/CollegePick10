using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Models
{
    public class ViewPicksModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalPoints { get; set; }
        public int Rank { get; set; }
        public bool MyPicks { get; set; }
        public int WeekNumber { get; set; }
        public int? MaxWeeks { get; set; }
        public List<ScheduleModel> Picks { get; set; }
    }
}
