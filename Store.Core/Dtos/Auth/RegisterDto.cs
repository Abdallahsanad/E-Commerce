using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Dtos.Auth
{
    public class RegisterDto
    {   
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public string Phone { get; set; }



        [Required(ErrorMessage = "DisplayName Is Required")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword Is Not Match Password")]
        public string ConfirmPassword { get; set; }
    }
}
