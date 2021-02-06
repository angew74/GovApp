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
        public VotiListaStorico()
        {
            ProfiloVoti = new HashSet<ProfiloVoti>();
        }

        [Key]      
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
        [ForeignKey("VotigeneraliStorico")]
        public int VotigeneraliStoricoid { get; set; }
        [Required]
        [Column]
        public DateTime Dataoperazioneold { get; set; }
        [Required]
        [Column]
        public string Utenteoperazioneold { get; set; }

        [Required]
        [Column]
        public int VotiSindacoStoricoid { get; set; }

        public virtual Liste Lista { get; set; }
        public virtual Sezioni Sezione { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }     
        public virtual VotiGeneraliStorico VotiGeneraliStorico { get; set; }
        public virtual VotiSindacoStorico VotiSindacoStorico { get; set; }
        public virtual ICollection<ProfiloVoti> ProfiloVoti { get; set; }
    }
}
