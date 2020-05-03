
using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace Gov.Core
{
    [Table("Coalizione")]
    public class Coalizione : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Column]
        [Required]
        [MaxLength(256)]
        public string Denominazione { get; set; }
    }
}