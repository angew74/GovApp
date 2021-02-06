using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Voti_SindacoStorico")]
    public partial class VotiSindacoStorico : AuditableEntity<int>
    {

        public VotiSindacoStorico()
        {
            ProfiloVoti = new HashSet<ProfiloVoti>();
            VotiLista = new HashSet<VotiListaStorico>();
        }

        [Key]        
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
        [ForeignKey("VotigeneraliStorico")]
        public int VotigeneraliStoricoid { get; set; }
        [Required]
        [Column]
        public string UtenteOperazioneOld { get; set; }
        [Required]
        [Column]
        public DateTime DataOperazioneOld { get; set; }
       

        public virtual Sezioni Sezione { get; set; }
        public virtual Sindaci Sindaco { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
       
        public virtual VotiGeneraliStorico VotigeneraliStorico { get; set; }
        public virtual ICollection<ProfiloVoti> ProfiloVoti { get; set; }
        public virtual ICollection<VotiListaStorico> VotiLista { get; set; }
    }
}
