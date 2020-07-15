using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{

    [Table("Plessi")]
    public partial class Plessi : Entity<int>
    {
        public Plessi()
        {
            Affluenze = new HashSet<Affluenze>();
            Sezioni = new HashSet<Sezioni>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public string Descrizione { get; set; }
        [Required]
        [Column]
        public string Ubicazione { get; set; }
    
        [Column]
        public int? Municipio { get; set; }
        [Required]
        [Column]
        public int Idtipoelezione { get; set; }

        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
        public virtual ICollection<Affluenze> Affluenze { get; set; }
        public virtual ICollection<Sezioni> Sezioni { get; set; }
    }
}
