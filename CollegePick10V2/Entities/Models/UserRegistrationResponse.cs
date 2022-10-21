using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities.Models
{
    public class UserRegistrationResponse
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
