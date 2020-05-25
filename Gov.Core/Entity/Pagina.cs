using Gov.Core.Identity;
using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core.Entity
{
    [Table("Pagina")]
    public class Pagina : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }


        [Column]
        [Required]
        [MaxLength(256)]
        public string Denominazione { get; set; }


        [Column]
        [Required]
        [MaxLength(10)]
        public string Codice { get; set; }        
       
        public int RoleId { get; set; }
        public virtual ApplicationRole Role { get; set; }
       
       
    }
}
