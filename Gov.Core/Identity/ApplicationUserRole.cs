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
     /*  
        [Required]
        [Column]
        public override int UserId { get; set; }     */

    }
}
