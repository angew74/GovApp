using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Voti_Sindaco")]
    public partial class VotiSindaco : AuditableEntity<int>
    {
        public VotiSindaco()
        {
            ProfiloVoti = new HashSet<ProfiloVoti>();
            VotiLista = new HashSet<VotiLista>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }
        [Required]
        [Column]
        public int Sindacoid { get; set; }
        [Required]
        [Column]
        public int Sezioneid { get; set; }
        [Required]
        [Column]
        public int Municipio { get; set; }
        [Required]
        [Column]
        public int NumeroVoti { get; set; }
        [Required]
        [Column]
        public int NumeroVotiSoloSindaco { get; set; }
        [Required]
        [Column]
        public int Votigeneraliid { get; set; }
      

        public virtual Sezioni Sezione { get; set; }
        public virtual Sindaci Sindaco { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
        public virtual VotiGenerali Votigenerali { get; set; }
        public virtual ICollection<ProfiloVoti> ProfiloVoti { get; set; }
        public virtual ICollection<VotiLista> VotiLista { get; set; }
    }
}
