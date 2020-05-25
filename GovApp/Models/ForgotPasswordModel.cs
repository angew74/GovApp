
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Models
{
    public class ForgotPasswordModel
    {

        [Required]
        [EmailAddress]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]       
        [UIHint("UserName")]
        public string UserName { get; set; }
    }
}
