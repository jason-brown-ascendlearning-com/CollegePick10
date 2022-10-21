using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Models
{
    public class UserProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone]
        public string PhoneNumber { get; set; }

        public string TimeZone { get; set; }
    }
}
