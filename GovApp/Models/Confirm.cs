using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Models
{
    public class Confirm
    {

        [Required]
        [EmailAddress]
        [UIHint("email")]
        public string Email { get; set; }

        public string Url { get; set; }
      
        [UIHint("UserName")]
        public string UserName { get; set; }

        [Required]        
        [UIHint("Id")]
        public string Id { get; set; }
      
        public bool Result { get; set; }

        [Required]
        [UIHint("password")]
        [StringLength(8, ErrorMessage = "{0} lungezza deve essere tra {2} and {1}.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [UIHint("conferma password")]
        [StringLength(8, ErrorMessage = "{0} lungezza deve essere tra {2} and {1}.", MinimumLength = 8)]
        public string ConfirmPassword { get; set; }
    }
}
