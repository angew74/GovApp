using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Identity
{

   
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        [NotMapped]
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
        /*  
           [Required]
           [Column]
           public override int UserId { get; set; }     */

    }
}
