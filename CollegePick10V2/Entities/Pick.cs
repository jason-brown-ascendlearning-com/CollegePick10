using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities
{
    public class Pick
    {
        [Key]
        public int PickID { get; set; }
        public string UserID { get; set; }
        public int GameID { get; set; }
        public int PickTypeID { get; set; }
        public int CorrectPoints { get; set; }
    }
}
