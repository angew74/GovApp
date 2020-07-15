using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Voti_Generali")]
    public partial class VotiGenerali : AuditableEntity<int>
    {
        public VotiGenerali()
        {
            ProfiloVoti = new HashSet<ProfiloVoti>();
            VotiLista = new HashSet<VotiLista>();
            VotiSindaco = new HashSet<VotiSindaco>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Sezioneid { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }
        [Required]
        [Column]
        public int Municipio { get; set; }
        [Required]
        [Column]
        public int Contestate { get; set; }
        [Required]
        [Column]
        public int Bianche { get; set; }
        [Required]
        [Column]
        public int Nulle { get; set; }
        [Required]
        [Column]
        public int TotaleValide { get; set; }
        [Required]
        [Column]
        public int SoloSindaco { get; set; }
        [Required]
        [Column]
        public int Totale { get; set; }
      

        public virtual Sezioni Sezione { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
        public virtual ICollection<ProfiloVoti> ProfiloVoti { get; set; }
        public virtual ICollection<VotiLista> VotiLista { get; set; }
        public virtual ICollection<VotiSindaco> VotiSindaco { get; set; }
    }
}
