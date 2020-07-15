using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Profilo_Voti")]
    public partial class ProfiloVoti : Entity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Column]
        public int? Votigeneraliid { get; set; }
        [Column]
        public int? Votisindacoid { get; set; }
        [Column]
        public int? Votilistaid { get; set; }
        [Required]
        [Column]
        public int Sezioneid { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }

        public virtual Sezioni Sezione { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
        public virtual VotiGenerali Votigenerali { get; set; }
        public virtual VotiLista Votilista { get; set; }
        public virtual VotiSindaco Votisindaco { get; set; }
    }
}
