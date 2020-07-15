using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Affluenze_Storico")]
    public partial class AffluenzeStorico : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }
        [Required]
        [Column]
        public int Sezioneid { get; set; }
        public int? Plessoid { get; set; }
        [Required]
        [Column]
        public int Iscrittiid { get; set; }
        [Column]
        public int? Costituzione1 { get; set; }
        [Column]
        public int? Costituzione2 { get; set; }
        [Column]
        public int? Apertura1 { get; set; }
        [Column]
        public int? Apertura2 { get; set; }
        [Column]
        public int? Apertura3 { get; set; }
        [Column]
        public int? Affluenza1 { get; set; }
        [Column]
        public int? Affluenza2 { get; set; }
        [Column]
        public int? Affluenza3 { get; set; }
        [Column]
        public int? Affluenza4 { get; set; }
        [Column]
        public int? Affluenza5 { get; set; }
        [Column]
        public int? Votantimaschi1 { get; set; }
        [Column]
        public int? Votantimaschi2 { get; set; }
        [Column]
        public int? Votantimaschi3 { get; set; }
        [Column]
        public int? Votantimaschi4 { get; set; }
        [Column]
        public int? Votantimaschi5 { get; set; }
        [Column]
        public int? Votantifemmine1 { get; set; }
        [Column]
        public int? Votantifemmine2 { get; set; }

        [Column]
        public int? Votantifemmine3 { get; set; }
        [Column]
        public int? Votantifemmine4 { get; set; }
        [Column]
        public int? Votantifemmine5 { get; set; }
        [Column]
        public int? Votantitotali1 { get; set; }
        [Column]
        public int? Votantitotali2 { get; set; }
        [Column]
        public int? Votantitotali3 { get; set; }
        [Column]
        public int? Votantitotali4 { get; set; }
        [Column]
        public int? Votantitotali5 { get; set; }
        public DateTime DataOperazioneold { get; set; }
        public string UtenteOperazioneold { get; set; }       
        public virtual Iscritti Iscritti { get; set; }
        public virtual Plessi Plesso { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
    }
}
