using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Liste")]
    public partial class Liste : Entity<int>
    {
        public Liste()
        {
            Candidati = new HashSet<Candidati>();
            RicalcoloPreferenze = new HashSet<RicalcoloPreferenze>();
            RicalcoloVotiLista = new HashSet<RicalcoloVotiLista>();
            VotiLista = new HashSet<VotiLista>();
            VotiListaStorico = new HashSet<VotiListaStorico>();
            VotiPeferenzeStorico = new HashSet<VotiPeferenzeStorico>();
            VotiPreferenze = new HashSet<VotiPreferenze>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }
        [Required]
        [Column]
        public string Denominazione { get; set; }
        [Required]
        [Column]
        public string DenominazioneBreve { get; set; }
     
        [Column]
        public int? ProgressivoManifesto { get; set; }
      
        [Column]
        public int? Progressivo { get; set; }
      
        [Column]
        public int? Coalizioneid { get; set; }
       
        [Column]
        public int? ProgressivoCoalizione { get; set; }
       
        [Column]
        public int? Sindacoid { get; set; }

        public virtual Raggruppamento Raggruppamenti { get; set; }
        public virtual Sindaci Sindaco { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
        public virtual ICollection<Candidati> Candidati { get; set; }
        public virtual ICollection<RicalcoloPreferenze> RicalcoloPreferenze { get; set; }
        public virtual ICollection<RicalcoloVotiLista> RicalcoloVotiLista { get; set; }
        public virtual ICollection<VotiLista> VotiLista { get; set; }
        public virtual ICollection<VotiListaStorico> VotiListaStorico { get; set; }
        public virtual ICollection<VotiPeferenzeStorico> VotiPeferenzeStorico { get; set; }
        public virtual ICollection<VotiPreferenze> VotiPreferenze { get; set; }
    }
}
