using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities
{
    public class PickType
    {
        [Key]
        public int PickTypeID { get; set; }
        public string PickTypeName { get; set; }
    }
}
