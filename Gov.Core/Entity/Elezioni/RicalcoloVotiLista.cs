using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Ricalcoli_VotiLista")]
    public partial class RicalcoloVotiLista : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Idtipoelezione { get; set; }
        [Required]
        [Column]
        public int Idtiporicalcolo { get; set; }
        [Required]
        [Column]
        public int Municipio { get; set; }
        [Required]
        [Column]
        public int Idlista { get; set; }
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
       
        [Column]
        public int IscrittiTotale { get; set; }
        [Required]
        [Column]
        public int VotantiPervenute { get; set; }
       
        [Column]
        public int VotantiTotali { get; set; }

        [Required]
        [Column]
        public string PercentualeVotantiPervenute { get; set; }
       
        [Column]
        public string PercentualeVotantiTotale { get; set; }
     

        [NotMapped]
        public string Denominazione { get; set; }

        public virtual Liste IdlistaNavigation { get; set; }
        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
        public virtual TipoRicalcolo IdtiporicalcoloNavigation { get; set; }
    }
}
