using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Ricalcoli_Affluenza")]
    public partial class RicalcoliAffluenza : AuditableEntity<int>
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
        public int AffluenzaTotale { get; set; }
        public int? IscrittiTotale { get; set; }
        [Required]
        [Column]
        public string PercentualeTotale { get; set; }
        [Required]
        [Column]
        public int AffluenzaMaschi { get; set; }
        [Required]
        [Column]
        public int IscrittiMaschi { get; set; }
        [Required]
        [Column]
        public string PercentualeMaschi { get; set; }
        [Required]
        [Column]
        public int AffluenzaFemmine { get; set; }
        [Required]
        [Column]
        public int IscrittiFemmine { get; set; }
        [Required]
        [Column]
        public string PercentualeFemmine { get; set; }
        [Required]
        [Column]
        public int NumeroAffluenza { get; set; }
    
        [Column]
        public int? NumeroSezioni { get; set; }
        [Required]
        [Column]
        public int TotaleSezioni { get; set; }
        [Required]
        [Column]
        public string PercentualeSezioniPervenute { get; set; }      

        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
        public virtual TipoRicalcolo IdtiporicalcoloNavigation { get; set; }

        [NotMapped]
        public string Sezione { get; set; }
    }
}
