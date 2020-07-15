using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Voti_Preferenze")]
    public partial class VotiPreferenze : AuditableEntity<int>
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
        [Required]
        [Column]
        public int Candidatoid { get; set; }
        [Required]
        [Column]
        public int Listaid { get; set; }
        [Required]
        [Column]
        public int Municipio { get; set; }
        [Required]
        [Column]
        public int Numerovoti { get; set; }
      

        public virtual Candidati Candidato { get; set; }
        public virtual Liste Lista { get; set; }
        public virtual Sezioni Sezione { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
    }
}
