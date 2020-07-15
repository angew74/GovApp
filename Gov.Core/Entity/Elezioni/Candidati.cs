using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Candidati")]
    public partial class Candidati : Entity<int>
    {
        public Candidati()
        {
            RicalcoloPreferenze = new HashSet<RicalcoloPreferenze>();
            VotiPeferenzeStorico = new HashSet<VotiPeferenzeStorico>();
            VotiPreferenze = new HashSet<VotiPreferenze>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public override int Id { get; set; }
        [Required]
        [Column]
        public string NomeCandidato { get; set; }
        [Required]
        [Column]
        public string CognomeCandidato { get; set; }
        [Required]
        [Column]
        public string SessoCandidato { get; set; }
        [Required]
        [Column]
        public int Progressivo { get; set; }
        [Required]
        [Column]
        public int Listaid { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }

        public virtual Liste Lista { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
        public virtual ICollection<RicalcoloPreferenze> RicalcoloPreferenze { get; set; }
        public virtual ICollection<VotiPeferenzeStorico> VotiPeferenzeStorico { get; set; }
        public virtual ICollection<VotiPreferenze> VotiPreferenze { get; set; }
    }
}
