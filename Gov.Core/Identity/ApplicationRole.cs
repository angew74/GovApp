using Gov.Core.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gov.Core.Identity
{
   
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; }       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }   
        public virtual ICollection<VoceMenu> VociMenu { get; set; }

        public virtual ICollection<Pagina> Pagine { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
