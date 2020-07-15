using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Tipo_Ricalcolo")]
    public partial class TipoRicalcolo : Entity<int>
    {
        public TipoRicalcolo()
        {
            RicalcoliAffluenza = new HashSet<RicalcoliAffluenza>();
            RicalcoliAperturaCostituzione = new HashSet<RicalcoloAperturaCostituzione>();
            RicalcoloPreferenze = new HashSet<RicalcoloPreferenze>();
            RicalcoloVotiCoalizioni = new HashSet<RicalcoloVotiRaggruppamento>();
            RicalcoloVotiLista = new HashSet<RicalcoloVotiLista>();
            RicalcoloVotiSindaco = new HashSet<RicalcoloVotiSindaco>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public string Descrizione { get; set; }
        [Required]
        [Column]
        public string CodiceFase { get; set; }
        [Required]
        [Column]
        public string Codice { get; set; }
        [Required]
        [Column]
        public int Idtipoelezione { get; set; }

        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
        public virtual ICollection<RicalcoliAffluenza> RicalcoliAffluenza { get; set; }
        public virtual ICollection<RicalcoloAperturaCostituzione> RicalcoliAperturaCostituzione { get; set; }
        public virtual ICollection<RicalcoloPreferenze> RicalcoloPreferenze { get; set; }
        public virtual ICollection<RicalcoloVotiRaggruppamento> RicalcoloVotiCoalizioni { get; set; }
        public virtual ICollection<RicalcoloVotiLista> RicalcoloVotiLista { get; set; }
        public virtual ICollection<RicalcoloVotiSindaco> RicalcoloVotiSindaco { get; set; }
    }
}
