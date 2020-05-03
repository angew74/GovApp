using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core
{
    [Table("Governo")]
    public class Governo : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public virtual Premier Premier { get; set; }

        public virtual Legislatura Legislatura { get; set; }
        [Column]
        public int NumeroMinisteri { get; set; }
        [Column]
        public int NumeroMinisteriSenzaPortafogio { get; set; }
        public virtual List<Dicastero> Dicasteri { get; set; }
        [Column]
        public DateTime? DataIncarico { get; set; }
        [Column]
        [Required]
        public int NumeroVotiSenato { get; set; }
        [Column]
        [Required]
        public int NumeroVotiCamera { get; set; }
        [Column]
        [Required]
        public DateTime DataFiducia { get; set; }
        [Column]
        [Required]
        public bool IsFiducia { get; set; }
        [Column]
        public DateTime? DataRevoca { get; set; }
    }
}
