using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Dtos.Auth
{
    public class UserDto
    {
        [Required(ErrorMessage = "DisplayName Is Required")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
