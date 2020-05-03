using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core
{
    [Table("Senato")]
    public class Senato : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Column]
        [Required]
        public DateTime DataInizio { get; set; }
        [Column]     
        public DateTime? DataFine { get; set; }

        public List<Partito> Partiti { get; set; }
    }
}
