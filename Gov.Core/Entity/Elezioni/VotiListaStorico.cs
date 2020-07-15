using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Voti_ListaStorico")]
    public partial class VotiListaStorico : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Listaid { get; set; }
        [Required]
        [Column]
        public int Sezioneid { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }
  
        [Column]
        public int? Municipio { get; set; }
        [Required]
        [Column]
        public int Voti { get; set; }
        [Required]
        [Column]
        public int Votigeneraliid { get; set; }
        [Required]
        [Column]
        public DateTime Dataoperazioneold { get; set; }
        [Required]
        [Column]
        public string Utenteoperazioneold { get; set; }
       

        public virtual Liste Lista { get; set; }
        public virtual Sezioni Sezione { get; set; }
    }
}
