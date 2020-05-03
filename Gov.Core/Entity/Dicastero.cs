using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core
{
    [Table("Dicastero")]
    public class Dicastero : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public virtual Ministero Ministero { get; set; }

        public virtual Ministro Ministro { get; set; }
        [Column]
        [Required]
        public DateTime DataInizio { get; set; }
        [Column]
        [Required]
        public DateTime DataFine { get; set; }
        [Column]
        public DateTime? DataGiuramento { get; set; }
        [Column]
        public DateTime? DataSfiducia { get; set; }


    }
}
