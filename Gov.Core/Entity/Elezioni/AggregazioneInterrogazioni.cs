using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Aggregazioni_Interrogazioni")]
    public partial class AggregazioneInterrogazioni : Entity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public string Codice { get; set; }
        [Required]
        [Column]
        public string Descrizione { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }

        public virtual Tipoelezione Tipoelezione { get; set; }
    }
}
