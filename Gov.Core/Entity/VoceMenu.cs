using Gov.Core.Identity;
using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core.Entity
{
    [Table("VoceMenu")]
    public class VoceMenu : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }


        [Column]
        [Required]
        [MaxLength(256)]
        public string Link { get; set; }


        [Column]
        [Required]
        [MaxLength(50)]
        public string Icona { get; set; }

        [Column]
        [Required]
        [MaxLength(100)]
        public string Voce { get; set; }

        [Column]
        [Required]       
        public bool Active { get; set; }
        [Column]
        public int RoleId { get; set; }

        public virtual ApplicationRole Role { get; set; }

        
    }
}
