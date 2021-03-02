using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrilledCommon.Models
{
    public class RegistrationModel
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
