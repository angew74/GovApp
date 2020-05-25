using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Identity
{
    [Table("Roles")]
    public partial class Roles : AuditableEntity<int>
    {
        public Roles()
        {
            Roleclaims = new HashSet<Roleclaims>();
            UserRoles = new HashSet<UserRoles>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public string Name { get; set; }
        [Required]
        [Column]
        public string NormalizedName { get; set; }
        [Required]
        [Column]
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<Roleclaims> Roleclaims { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
