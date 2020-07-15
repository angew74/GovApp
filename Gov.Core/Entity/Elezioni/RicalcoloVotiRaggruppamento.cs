using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{

    [Table("Ricalcoli_VotiRaggruppamento")]
    public partial class RicalcoloVotiRaggruppamento : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }
        [Required]
        [Column]
        public int Tiporicalcoloid { get; set; }
        [Required]
        [Column]
        public int Municipio { get; set; }
        [Required]
        [Column]
        public int Coalizioneid { get; set; }
        [Required]
        [Column]
        public int NumeroVoti { get; set; }
        [Required]
        [Column]
        public string PercentualeVoti { get; set; }
        [Required]
        [Column]
        public int NumeroSezioni { get; set; }
        [Required]
        [Column]
        public int TotaleSezioni { get; set; }
        [Required]
        [Column]
        public string PercentualeSezioniPervenute { get; set; }
        [Required]
        [Column]
        public int IscrittiPervenute { get; set; }
        [Required]
        [Column]
        public int IscrittiTotale { get; set; }
        [Required]
        [Column]
        public int VotantiPervenute { get; set; }
        [Required]
        [Column]
        public int VotantiTotale { get; set; }
        [Required]
        [Column]
        public string PercentualeVotantiPervenute { get; set; }
        [Required]
        [Column]
        public string PercentualeVotantiTotale { get; set; }    

        public virtual Raggruppamento Raggruppamenti { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
        public virtual TipoRicalcolo Tiporicalcolo { get; set; }
    }
}
