using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Models
{
    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password must match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }

    }
}
