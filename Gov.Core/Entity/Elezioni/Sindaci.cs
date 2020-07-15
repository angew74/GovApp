using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Sindaci")]
    public partial class Sindaci : Entity<int>
    {
        public Sindaci()
        {
            Raggruppamenti = new HashSet<Raggruppamento>();
            Liste = new HashSet<Liste>();
            RicalcoloVotiSindaco = new HashSet<RicalcoloVotiSindaco>();
            VotiSindaco = new HashSet<VotiSindaco>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public string Nome { get; set; }
        [Required]
        [Column]
        public string Cognome { get; set; }
        [Required]
        [Column]
        public string Sesso { get; set; }
        [Required]
        [Column]
        public int Progressivo { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }

        public virtual Tipoelezione Tipoelezione { get; set; }
        public virtual ICollection<Raggruppamento> Raggruppamenti { get; set; }
        public virtual ICollection<Liste> Liste { get; set; }
        public virtual ICollection<RicalcoloVotiSindaco> RicalcoloVotiSindaco { get; set; }
        public virtual ICollection<VotiSindaco> VotiSindaco { get; set; }
    }
}
