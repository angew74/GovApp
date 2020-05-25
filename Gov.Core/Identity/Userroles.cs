using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Identity
{
    
    [Table("UserRoles")]
    public partial class UserRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column]
        public int UserId { get; set; }
        [Required]
        [Column]
        public int RoleId { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual Users User { get; set; }
    }
}
