using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core
{
    [Table("Legislatura")]
    public class Legislatura  : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Column]
        [Required]
        public int Numero { get; set; }
        [Column]
        [Required]
        public DateTime DataInizio { get; set; }
        [Column]
        [Required]
        public DateTime DataFine { get; set; }
        public Senato Senato { get; set; }
        public Camera Camera { get; set; }
    }
}
