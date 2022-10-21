using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Models
{
    public class UserRegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Timezone is required")]
        public string TimeZone { get; set; }

        [Required(ErrorMessage = "Invitation code is required")]
        public string InviteCode { get; set; }
    }
}
