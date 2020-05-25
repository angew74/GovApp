using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Models
{
    public class ChangePasswordModel
    {

        [Required]       
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password attuale")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "La {0} deve essere almeno di {2} caratteri e al massimo di {1}.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "nuova password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "conferna nuova password")]
        [Compare("NewPassword", ErrorMessage = "La password e la conferma non coincidono.")]
        public string ConfirmPassword { get; set; }
    }
}
