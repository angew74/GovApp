using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core.Entity
{
    [Table("TipoContenuto")]
    public class TipoContenuto : AuditableEntity<int>
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }


        [Column]
        [Required]
        [MaxLength(10)]
        public string Codice { get; set; }

    }
}
