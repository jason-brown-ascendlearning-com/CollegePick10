using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Models
{
    public class PickModel
    {
        public int PickCount { get; set; }
        public bool IsUserLocked { get; set; }
        public List<ScheduleModel> Schedule { get; set; }
    }
}
